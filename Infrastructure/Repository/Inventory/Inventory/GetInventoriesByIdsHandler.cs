using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetInventoriesByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetInventoriesByIdsQuery, List<GetInventoryResponseDTO>>
    {
        public async Task<List<GetInventoryResponseDTO>> Handle(GetInventoriesByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var inventoriesFound = await wmsDbContext.Inventories.AsNoTracking()
                .Where(inventory => request.InventoryIds.Contains(inventory.Id))
                .Include(inventory => inventory.Product)
                .ToListAsync(cancellationToken);

            var result = new List<GetInventoryResponseDTO>();
            foreach (var inventoryFound in inventoriesFound)
            {
                var getInventoryResponseDTO = inventoryFound.Adapt<GetInventoryResponseDTO>();
                getInventoryResponseDTO.ProductId = inventoryFound.ProductId;
                result.Add(getInventoryResponseDTO);
            }
            
            return result;
        }
    }
}
