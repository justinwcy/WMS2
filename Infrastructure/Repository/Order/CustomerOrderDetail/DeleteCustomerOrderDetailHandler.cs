using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteCustomerOrderDetailHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteCustomerOrderDetailCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteCustomerOrderDetailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundCustomerOrderDetail = await wmsDbContext.CustomerOrderDetails.FirstOrDefaultAsync(
                    customerOrderDetail => customerOrderDetail.Id == request.Id,
                    cancellationToken);
                if (foundCustomerOrderDetail == null)
                {
                    return GeneralDbResponses.ItemNotFound("CustomerOrderDetail");
                }

                wmsDbContext.CustomerOrderDetails.Remove(foundCustomerOrderDetail);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("CustomerOrderDetail");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    