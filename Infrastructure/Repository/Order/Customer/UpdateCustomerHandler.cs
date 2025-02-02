using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

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

                customerFound.Address = request.Model.Address;
                customerFound.Email = request.Model.Email;
                customerFound.FirstName = request.Model.FirstName;
                customerFound.LastName = request.Model.LastName;
                
                var customerOrdersToAdd = await wmsDbContext.CustomerOrders
                    .Where(customerOrder => request.Model.CustomerOrderIds.Contains(customerOrder.Id))
                    .ToListAsync(cancellationToken);
                customerFound.CustomerOrders.RemoveAll(customerOrder => !request.Model.CustomerOrderIds.Contains(customerOrder.Id));
                foreach (var customerOrderToAdd in customerOrdersToAdd)
                {
                    if (customerFound.CustomerOrders.All(customerOrder => customerOrder.Id != customerOrderToAdd.Id))
                    {
                        customerFound.CustomerOrders.Add(customerOrderToAdd);
                    }
                }

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
    