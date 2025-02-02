using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

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

                customerOrderFound.ExpectedArrivalDate = request.Model.ExpectedArrivalDate;
                customerOrderFound.OrderAddress = request.Model.OrderAddress;
                customerOrderFound.OrderCreationDate = request.Model.OrderCreationDate;

                var binFound = await wmsDbContext.Bins.FirstOrDefaultAsync(
                    bin => bin.Id == request.Model.BinId,
                    cancellationToken);
                if (binFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Bin");
                }
                customerOrderFound.Bin = binFound;
                customerOrderFound.BinId = request.Model.BinId;

                var courierFound = await wmsDbContext.Couriers.FirstOrDefaultAsync(
                    courier => courier.Id == request.Model.CourierId,
                    cancellationToken);
                if (courierFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Courier");
                }
                customerOrderFound.Courier = courierFound;
                customerOrderFound.CourierId = request.Model.CourierId;

                var customerFound = await wmsDbContext.Customers.FirstOrDefaultAsync(
                    customer => customer.Id == request.Model.CustomerId,
                    cancellationToken);
                if (customerFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Customer");
                }
                customerOrderFound.Customer = customerFound;
                customerOrderFound.CustomerId = request.Model.CustomerId;

                var customerOrderDetailsToAdd = await wmsDbContext.CustomerOrderDetails
                    .Where(customerOrderDetail => request.Model.CustomerOrderDetailIds.Contains(customerOrderDetail.Id))
                    .ToListAsync(cancellationToken);
                customerOrderFound.CustomerOrderDetails.RemoveAll(customerOrderDetail => !request.Model.CustomerOrderDetailIds.Contains(customerOrderDetail.Id));
                foreach (var customerOrderDetailToAdd in customerOrderDetailsToAdd)
                {
                    if (customerOrderFound.CustomerOrderDetails.All(customerOrderDetail => customerOrderDetail.Id != customerOrderDetailToAdd.Id))
                    {
                        customerOrderFound.CustomerOrderDetails.Add(customerOrderDetailToAdd);
                    }
                }

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
    