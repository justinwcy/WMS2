using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteLocationHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteLocationCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundLocation = await wmsDbContext.Locations.FirstOrDefaultAsync(
                    location => location.Id == request.Id,
                    cancellationToken);
                if (foundLocation == null)
                {
                    return GeneralDbResponses.ItemNotFound("Location");
                }

                wmsDbContext.Locations.Remove(foundLocation);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("Location");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    