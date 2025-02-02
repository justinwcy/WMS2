using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateZoneHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateZoneCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateZoneCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var zoneFound = await wmsDbContext.Zones
                    .Include(zone=>zone.Warehouse)
                    .Include(zone=>zone.Racks)
                    .Include(zone=>zone.Staffs)
                    .FirstOrDefaultAsync(zone => zone.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (zoneFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Zone");
                }

                // update plain data
                zoneFound.Name = request.Model.Name;

                // update relationships
                var warehouseFound = wmsDbContext.Warehouses.FirstOrDefault(warehouse => warehouse.Id == request.Model.Id);
                if (warehouseFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Warehouse");
                }
                zoneFound.Warehouse = warehouseFound;

                var racksToAdd = await wmsDbContext.Racks
                    .Where(rack => request.Model.RackIds.Contains(rack.Id))
                    .ToListAsync(cancellationToken);
                zoneFound.Racks.RemoveAll(r => !request.Model.RackIds.Contains(r.Id));
                foreach (var rackToAdd in racksToAdd)
                {
                    if (zoneFound.Racks.All(rack => rack.Id != rackToAdd.Id))
                    {
                        zoneFound.Racks.Add(rackToAdd);
                    }
                }

                var staffsToAdd = await wmsDbContext.Staffs
                    .Where(staff => request.Model.StaffIds.Contains(staff.Id))
                    .ToListAsync(cancellationToken);
                zoneFound.Staffs.RemoveAll(s => !request.Model.StaffIds.Contains(s.Id));
                foreach (var staffToAdd in staffsToAdd)
                {
                    if (zoneFound.Staffs.All(staff => staff.Id != staffToAdd.Id))
                    {
                        zoneFound.Staffs.Add(staffToAdd);
                    }
                }

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Zone");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    