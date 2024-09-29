using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

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
                var refundOrderFound = await wmsDbContext.RefundOrders.FirstOrDefaultAsync(
                    refundOrder => refundOrder.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (refundOrderFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("RefundOrder");
                }

                wmsDbContext.Entry(refundOrderFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<RefundOrder>();
                wmsDbContext.RefundOrders.Update(adaptData);
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
    