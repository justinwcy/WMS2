using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteRackHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteRackCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteRackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundRack = await wmsDbContext.Racks.FirstOrDefaultAsync(
                    rack => rack.Id == request.Id,
                    cancellationToken);
                if (foundRack == null)
                {
                    return GeneralDbResponses.ItemNotFound("Rack");
                }

                wmsDbContext.Racks.Remove(foundRack);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("Rack");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    