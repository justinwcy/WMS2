using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllWarehouseByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllWarehouseByCompanyIdQuery, IEnumerable<GetWarehouseResponseDTO>>
    {
        public async Task<IEnumerable<GetWarehouseResponseDTO>> Handle(GetAllWarehouseByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var warehouses = await wmsDbContext.Warehouses
                .AsNoTracking()
                .Where(warehouse => staffIdsInCompany.Contains(warehouse.CreatedBy))
                .Include(warehouse=> warehouse.Zones)
                .ToListAsync(cancellationToken);

            var warehousesDto = warehouses.Adapt<List<GetWarehouseResponseDTO>>();

            foreach (var warehouseDto in warehousesDto)
            {
                warehouseDto.ZoneIds = warehouses
                    .First(warehouse=>warehouse.Id == warehouseDto.Id)
                    .Zones
                    .Select(zone => zone.Id).ToList();
            }

            return warehousesDto;
        }
    }
}
    