using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateCustomerHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateCustomerCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var customerFound = await wmsDbContext.Customers.FirstOrDefaultAsync(
                    customer => customer.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (customerFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Customer");
                }

                wmsDbContext.Entry(customerFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<Customer>();
                wmsDbContext.Customers.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Customer");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    