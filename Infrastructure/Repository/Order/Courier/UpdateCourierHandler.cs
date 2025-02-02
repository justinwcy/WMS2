using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateCourierHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateCourierCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateCourierCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var courierFound = await wmsDbContext.Couriers.FirstOrDefaultAsync(
                    courier => courier.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (courierFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Courier");
                }

                courierFound.Name = request.Model.Name;
                courierFound.Price = request.Model.Price;
                courierFound.Remark = request.Model.Remark;

                var customerOrdersToAdd = await wmsDbContext.CustomerOrders
                    .Where(customerOrder => request.Model.CustomerOrderIds.Contains(customerOrder.Id))
                    .ToListAsync(cancellationToken);
                courierFound.CustomerOrders.RemoveAll(customerOrder => !request.Model.CustomerOrderIds.Contains(customerOrder.Id));
                foreach (var customerOrderToAdd in customerOrdersToAdd)
                {
                    if (courierFound.CustomerOrders.All(customerOrder => customerOrder.Id != customerOrderToAdd.Id))
                    {
                        courierFound.CustomerOrders.Add(customerOrderToAdd);
                    }
                }

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Courier");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    