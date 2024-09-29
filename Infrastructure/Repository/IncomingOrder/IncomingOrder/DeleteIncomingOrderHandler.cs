using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteIncomingOrderHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteIncomingOrderCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteIncomingOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundIncomingOrder = await wmsDbContext.IncomingOrders.FirstOrDefaultAsync(
                    incomingOrder => incomingOrder.Id == request.Id,
                    cancellationToken);
                if (foundIncomingOrder == null)
                {
                    return GeneralDbResponses.ItemNotFound("IncomingOrder");
                }

                wmsDbContext.IncomingOrders.Remove(foundIncomingOrder);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("IncomingOrder");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    