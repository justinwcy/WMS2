using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductGroupsByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductGroupsByIdsQuery, List<GetProductGroupResponseDTO>>
    {
        public async Task<List<GetProductGroupResponseDTO>> Handle(GetProductGroupsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productGroupsFound = await wmsDbContext.ProductGroups.AsNoTracking()
                .Where(productGroup => request.ProductGroupIds.Contains(productGroup.Id))
                .Include(productGroup => productGroup.Products)
                .Include(productGroup => productGroup.Photos)
                .ToListAsync(cancellationToken);

            var result = new List<GetProductGroupResponseDTO>();
            foreach (var productGroupFound in productGroupsFound)
            {
                var getProductGroupResponseDTO = productGroupFound.Adapt<GetProductGroupResponseDTO>();
                getProductGroupResponseDTO.ProductIds = productGroupFound.Products.Select(product => product.Id).ToList();
                getProductGroupResponseDTO.PhotoIds = productGroupFound.Photos.Select(photo => photo.Id).ToList();
                result.Add(getProductGroupResponseDTO);
            }
            
            return result;
        }
    }
}
