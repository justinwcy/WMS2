using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateCustomerOrderDetailHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateCustomerOrderDetailCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateCustomerOrderDetailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var customerOrderDetailFound = await wmsDbContext.CustomerOrderDetails.FirstOrDefaultAsync(
                    customerOrderDetail => customerOrderDetail.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (customerOrderDetailFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("CustomerOrderDetail");
                }
                
                customerOrderDetailFound.Status = request.Model.Status;
                customerOrderDetailFound.Quantity = request.Model.Quantity;

                var productFound = await wmsDbContext.Products.FirstOrDefaultAsync(
                    product => product.Id == request.Model.ProductId,
                    cancellationToken);
                if (productFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Product");
                }
                customerOrderDetailFound.Product = productFound;
                customerOrderDetailFound.ProductId = request.Model.ProductId;

                var customerOrderFound = await wmsDbContext.CustomerOrders.FirstOrDefaultAsync(
                    customerOrder => customerOrder.Id == request.Model.CustomerOrderId,
                    cancellationToken);
                if (customerOrderFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("CustomerOrder");
                }
                customerOrderDetailFound.CustomerOrder = customerOrderFound;
                customerOrderDetailFound.CustomerOrderId = request.Model.CustomerOrderId;

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("CustomerOrderDetail");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    