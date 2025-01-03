using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateShopHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateShopCommand, CreateShopResponseDTO>
    {
        public async Task<CreateShopResponseDTO> Handle(CreateShopCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<Shop>();
            var shopCreated = wmsDbContext.Shops.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateShopResponseDTO() { Id = shopCreated.Entity.Id};
        }
    }
}
    