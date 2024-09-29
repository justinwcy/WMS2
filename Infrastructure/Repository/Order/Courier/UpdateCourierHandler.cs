using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateCourierHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateCourierCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateCourierCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var courierFound = await wmsDbContext.Couriers.FirstOrDefaultAsync(
                    courier => courier.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (courierFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Courier");
                }

                wmsDbContext.Entry(courierFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<Courier>();
                wmsDbContext.Couriers.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Courier");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    