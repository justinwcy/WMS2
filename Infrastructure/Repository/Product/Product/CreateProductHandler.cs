using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateProductHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateProductCommand, CreateProductResponseDTO>
    {
        public async Task<CreateProductResponseDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<Product>();
            var productCreated = wmsDbContext.Products.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateProductResponseDTO() { Id = productCreated.Entity.Id};
        }
    }
}
    