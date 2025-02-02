using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateRefundOrderHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateRefundOrderCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateRefundOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var refundOrderFound = await wmsDbContext.RefundOrders
                    .Include(refundOrder => refundOrder.RefundOrderProducts)
                    .FirstOrDefaultAsync(
                    refundOrder => refundOrder.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (refundOrderFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("RefundOrder");
                }

                refundOrderFound.Status = request.Model.Status;
                refundOrderFound.RefundDate = request.Model.RefundDate;
                refundOrderFound.RefundReason = request.Model.RefundReason;

                var refundOrderProductsToAdd = await wmsDbContext.RefundOrderProducts
                    .Where(refundOrderProduct => request.Model.RefundOrderProductIds.Contains(refundOrderProduct.Id))
                    .ToListAsync(cancellationToken);
                refundOrderFound.RefundOrderProducts.RemoveAll(refundOrderProduct => !request.Model.RefundOrderProductIds.Contains(refundOrderProduct.Id));
                foreach (var refundOrderProductToAdd in refundOrderProductsToAdd)
                {
                    if (refundOrderFound.RefundOrderProducts.All(refundOrderProduct => refundOrderProduct.Id != refundOrderProductToAdd.Id))
                    {
                        refundOrderFound.RefundOrderProducts.Add(refundOrderProductToAdd);
                    }
                }

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("RefundOrder");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    