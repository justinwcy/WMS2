using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateProductRackHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateProductRackCommand, CreateProductRackResponseDTO>
    {
        public async Task<CreateProductRackResponseDTO> Handle(CreateProductRackCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<ProductRack>();
            var productRackCreated = wmsDbContext.ProductRacks.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateProductRackResponseDTO() { Id = productRackCreated.Entity.Id};
        }
    }
}
    