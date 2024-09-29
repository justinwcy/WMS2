using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateIncomingOrderHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateIncomingOrderCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateIncomingOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var incomingOrderFound = await wmsDbContext.IncomingOrders.FirstOrDefaultAsync(
                    incomingOrder => incomingOrder.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (incomingOrderFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("IncomingOrder");
                }

                wmsDbContext.Entry(incomingOrderFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<IncomingOrder>();
                wmsDbContext.IncomingOrders.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("IncomingOrder");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    