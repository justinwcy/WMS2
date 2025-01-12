using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetWarehouseByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetWarehouseByIdQuery, GetWarehouseResponseDTO>
    {
        public async Task<GetWarehouseResponseDTO> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var warehouseFound = await wmsDbContext.Warehouses.AsNoTracking()
                .Include(warehouse => warehouse.Zones)
                .Include(warehouse => warehouse.Company)
                .FirstAsync(warehouse=>warehouse.Id == request.Id, cancellationToken);

            var result = warehouseFound.Adapt<GetWarehouseResponseDTO>();
            result.ZoneIds = warehouseFound.Zones.Select(zone=>zone.Id).ToList();
            result.CompanyId = warehouseFound.Company.Id;
            
            return result;
        }
    }
}
    