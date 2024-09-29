using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

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

                wmsDbContext.Entry(refundOrderProductFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<RefundOrderProduct>();
                wmsDbContext.RefundOrderProducts.Update(adaptData);
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
    