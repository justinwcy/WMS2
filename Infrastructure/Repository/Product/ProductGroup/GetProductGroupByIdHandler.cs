using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductGroupByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductGroupByIdQuery, GetProductGroupResponseDTO> 
    {
        public async Task<GetProductGroupResponseDTO> Handle(GetProductGroupByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productGroupFound = wmsDbContext.ProductGroups.AsNoTracking()
                .Include(productGroup => productGroup.Products)
                .First(productGroup=>productGroup.Id == request.Id);

            var result = productGroupFound.Adapt<GetProductGroupResponseDTO>();
            result.ProductIds = productGroupFound.Products.Select(product=>product.Id).ToList();
            
            return result;
        }
    }
}
    