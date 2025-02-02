using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateWarehouseHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateWarehouseCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var warehouseFound = await wmsDbContext.Warehouses.FirstOrDefaultAsync(
                    warehouse => warehouse.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (warehouseFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Warehouse");
                }

                warehouseFound.Address = request.Model.Address;
                warehouseFound.Name = request.Model.Name;

                var companyFound = await wmsDbContext.Companies
                    .FirstOrDefaultAsync(company => company.Id == request.Model.CompanyId, cancellationToken);
                if (companyFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Company");
                }
                warehouseFound.Company = companyFound;
                warehouseFound.CompanyId = request.Model.CompanyId;
                
                var zonesToAdd = await wmsDbContext.Zones
                    .Where(zone => request.Model.ZoneIds.Contains(zone.Id))
                    .ToListAsync(cancellationToken);
                warehouseFound.Zones.RemoveAll(zone => !request.Model.ZoneIds.Contains(zone.Id));
                foreach (var zoneToAdd in zonesToAdd)
                {
                    if (warehouseFound.Zones.All(zone => zone.Id != zoneToAdd.Id))
                    {
                        warehouseFound.Zones.Add(zoneToAdd);
                    }
                }

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Warehouse");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    