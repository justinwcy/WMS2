using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateCustomerOrderHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateCustomerOrderCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var customerOrderFound = await wmsDbContext.CustomerOrders.FirstOrDefaultAsync(
                    customerOrder => customerOrder.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (customerOrderFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("CustomerOrder");
                }

                wmsDbContext.Entry(customerOrderFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<CustomerOrder>();
                wmsDbContext.CustomerOrders.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("CustomerOrder");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    