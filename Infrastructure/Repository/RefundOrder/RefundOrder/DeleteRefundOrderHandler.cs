using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteRefundOrderHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteRefundOrderCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteRefundOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundRefundOrder = await wmsDbContext.RefundOrders.FirstOrDefaultAsync(
                    refundOrder => refundOrder.Id == request.Id,
                    cancellationToken);
                if (foundRefundOrder == null)
                {
                    return GeneralDbResponses.ItemNotFound("RefundOrder");
                }

                wmsDbContext.RefundOrders.Remove(foundRefundOrder);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("RefundOrder");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    