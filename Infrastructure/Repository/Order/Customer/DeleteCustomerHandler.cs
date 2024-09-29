using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteCustomerHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteCustomerCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundCustomer = await wmsDbContext.Customers.FirstOrDefaultAsync(
                    customer => customer.Id == request.Id,
                    cancellationToken);
                if (foundCustomer == null)
                {
                    return GeneralDbResponses.ItemNotFound("Customer");
                }

                wmsDbContext.Customers.Remove(foundCustomer);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("Customer");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    