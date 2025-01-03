using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateZoneHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateZoneCommand, CreateZoneResponseDTO>
    {
        public async Task<CreateZoneResponseDTO> Handle(CreateZoneCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<Zone>();
            var zoneCreated = wmsDbContext.Zones.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateZoneResponseDTO() { Id = zoneCreated.Entity.Id};
        }
    }
}
    