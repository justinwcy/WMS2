using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateRefundOrderProductHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateRefundOrderProductCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateRefundOrderProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var refundOrderProductFound = await wmsDbContext.RefundOrderProducts.FirstOrDefaultAsync(
                    refundOrderProduct => refundOrderProduct.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (refundOrderProductFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("RefundOrderProduct");
                }

                refundOrderProductFound.Status = request.Model.Status;
                refundOrderProductFound.Quantity = request.Model.Quantity;

                var refundOrderFound = await wmsDbContext.RefundOrders.FirstOrDefaultAsync(
                    refundOrder => refundOrder.Id == request.Model.RefundOrderId,
                    cancellationToken);
                if (refundOrderFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("RefundOrder");
                }
                refundOrderProductFound.RefundOrder = refundOrderFound;
                refundOrderProductFound.RefundOrderId = request.Model.RefundOrderId;

                var productFound = await wmsDbContext.Products.FirstOrDefaultAsync(
                    product => product.Id == request.Model.ProductId,
                    cancellationToken);
                if (productFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Product");
                }
                refundOrderProductFound.Product = productFound;
                refundOrderProductFound.ProductId = request.Model.ProductId;

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("RefundOrderProduct");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    