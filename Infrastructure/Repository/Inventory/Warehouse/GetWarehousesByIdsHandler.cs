using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetWarehousesByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetWarehousesByIdsQuery, List<GetWarehouseResponseDTO>>
    {
        public async Task<List<GetWarehouseResponseDTO>> Handle(GetWarehousesByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var warehousesFound = await wmsDbContext.Warehouses.AsNoTracking()
                .Include(warehouse => warehouse.Zones)
                .Include(warehouse => warehouse.Company)
                .Where(warehouse => request.WarehouseIds.Contains(warehouse.Id))
                .ToListAsync(cancellationToken);

            var result = new List<GetWarehouseResponseDTO>();
            foreach (var warehouseFound in warehousesFound)
            {
                var getWarehouseResponseDTO = warehouseFound.Adapt<GetWarehouseResponseDTO>();
                getWarehouseResponseDTO.ZoneIds = warehouseFound.Zones?.Select(zone => zone.Id).ToList();
                getWarehouseResponseDTO.CompanyId = warehouseFound.Company?.Id;
                result.Add(getWarehouseResponseDTO);
            }
            
            return result;
        }
    }
}
