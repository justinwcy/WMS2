using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllProductGroupProductByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllProductGroupProductByCompanyIdQuery, IEnumerable<GetProductGroupProductResponseDTO>>
    {
        public async Task<IEnumerable<GetProductGroupProductResponseDTO>> Handle(GetAllProductGroupProductByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var productGroupProducts = await wmsDbContext.ProductGroupProducts
                .AsNoTracking()
                .Where(productGroupProduct => staffIdsInCompany.Contains(productGroupProduct.CreatedBy))
                .ToListAsync(cancellationToken);

            var ProductGroupProductsDto = productGroupProducts.Adapt<List<GetProductGroupProductResponseDTO>>();
            return ProductGroupProductsDto;
        }
    }
}
    