using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateIncomingOrderProductHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateIncomingOrderProductCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateIncomingOrderProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var incomingOrderProductFound = await wmsDbContext.IncomingOrderProducts.FirstOrDefaultAsync(
                    incomingOrderProduct => incomingOrderProduct.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (incomingOrderProductFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("IncomingOrderProduct");
                }

                incomingOrderProductFound.Status = request.Model.Status;
                incomingOrderProductFound.Quantity = request.Model.Quantity;

                var productFound = await wmsDbContext.Products.FirstOrDefaultAsync(
                    product => product.Id == request.Model.ProductId, cancellationToken);
                if (productFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Product");
                }

                incomingOrderProductFound.Product = productFound;
                incomingOrderProductFound.ProductId = request.Model.ProductId;

                var incomingOrderFound = await wmsDbContext.IncomingOrders.FirstOrDefaultAsync(
                    incomingOrder => incomingOrder.Id == request.Model.IncomingOrderId, cancellationToken);
                if (incomingOrderFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("IncomingOrder");
                }

                incomingOrderProductFound.IncomingOrder = incomingOrderFound;
                incomingOrderProductFound.IncomingOrderId = request.Model.IncomingOrderId;

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("IncomingOrderProduct");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    