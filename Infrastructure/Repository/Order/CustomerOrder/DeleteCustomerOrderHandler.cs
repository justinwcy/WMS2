using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteCustomerOrderHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteCustomerOrderCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundCustomerOrder = await wmsDbContext.CustomerOrders.FirstOrDefaultAsync(
                    customerOrder => customerOrder.Id == request.Id,
                    cancellationToken);
                if (foundCustomerOrder == null)
                {
                    return GeneralDbResponses.ItemNotFound("CustomerOrder");
                }

                wmsDbContext.CustomerOrders.Remove(foundCustomerOrder);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("CustomerOrder");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    