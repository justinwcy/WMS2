using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateProductGroupHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateProductGroupCommand, CreateProductGroupResponseDTO>
    {
        public async Task<CreateProductGroupResponseDTO> Handle(CreateProductGroupCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<ProductGroup>();
            var productGroupCreated = wmsDbContext.ProductGroups.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateProductGroupResponseDTO() { Id = productGroupCreated.Entity.Id};
        }
    }
}
    