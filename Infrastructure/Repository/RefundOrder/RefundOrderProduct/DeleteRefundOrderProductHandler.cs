using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteRefundOrderProductHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteRefundOrderProductCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteRefundOrderProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundRefundOrderProduct = await wmsDbContext.RefundOrderProducts.FirstOrDefaultAsync(
                    refundOrderProduct => refundOrderProduct.Id == request.Id,
                    cancellationToken);
                if (foundRefundOrderProduct == null)
                {
                    return GeneralDbResponses.ItemNotFound("RefundOrderProduct");
                }

                wmsDbContext.RefundOrderProducts.Remove(foundRefundOrderProduct);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("RefundOrderProduct");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    