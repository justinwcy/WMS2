using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteCourierHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteCourierCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteCourierCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundCourier = await wmsDbContext.Couriers.FirstOrDefaultAsync(
                    courier => courier.Id == request.Id,
                    cancellationToken);
                if (foundCourier == null)
                {
                    return GeneralDbResponses.ItemNotFound("Courier");
                }

                wmsDbContext.Couriers.Remove(foundCourier);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("Courier");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    