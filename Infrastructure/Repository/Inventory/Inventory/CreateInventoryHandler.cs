using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateInventoryHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateInventoryCommand, CreateInventoryResponseDTO>
    {
        public async Task<CreateInventoryResponseDTO> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<Inventory>();
            var inventoryCreated = wmsDbContext.Inventories.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateInventoryResponseDTO() { Id = inventoryCreated.Entity.Id};
        }
    }
}
    