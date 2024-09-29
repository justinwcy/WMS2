using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

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

                wmsDbContext.Entry(incomingOrderProductFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<IncomingOrderProduct>();
                wmsDbContext.IncomingOrderProducts.Update(adaptData);
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
    