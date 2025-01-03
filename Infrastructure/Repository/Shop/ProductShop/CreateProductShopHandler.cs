using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateProductShopHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateProductShopCommand, CreateProductShopResponseDTO>
    {
        public async Task<CreateProductShopResponseDTO> Handle(CreateProductShopCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<ProductShop>();
            var productShopCreated = wmsDbContext.ProductShops.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateProductShopResponseDTO() { Id = productShopCreated.Entity.Id};
        }
    }
}
    