using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateProductGroupProductHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateProductGroupProductCommand, CreateProductGroupProductResponseDTO>
    {
        public async Task<CreateProductGroupProductResponseDTO> Handle(CreateProductGroupProductCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<ProductGroupProduct>();
            var productGroupProductCreated = wmsDbContext.ProductGroupProducts.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateProductGroupProductResponseDTO() { Id = productGroupProductCreated.Entity.Id};
        }
    }
}
    