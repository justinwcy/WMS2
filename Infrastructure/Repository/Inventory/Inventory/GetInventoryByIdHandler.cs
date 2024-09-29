using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetInventoryByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetInventoryByIdQuery, GetInventoryResponseDTO>
    {
        public async Task<GetInventoryResponseDTO> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var inventoryFound = await wmsDbContext.Inventories.AsNoTracking()
                .FirstAsync(inventory=>inventory.Id == request.Id, cancellationToken);

            var result = inventoryFound.Adapt<GetInventoryResponseDTO>();
            return result;
        }
    }
}
    