using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateWarehouseHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateWarehouseCommand, CreateWarehouseResponseDTO>
    {
        public async Task<CreateWarehouseResponseDTO> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<Warehouse>();
            var warehouseCreated = wmsDbContext.Warehouses.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateWarehouseResponseDTO() { Id = warehouseCreated.Entity.Id};
        }
    }
}
    