using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateZoneStaffHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateZoneStaffCommand, CreateZoneStaffResponseDTO>
    {
        public async Task<CreateZoneStaffResponseDTO> Handle(CreateZoneStaffCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<ZoneStaff>();
            var zoneStaffCreated = wmsDbContext.ZoneStaffs.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateZoneStaffResponseDTO() { Id = zoneStaffCreated.Entity.Id};
        }
    }
}
    