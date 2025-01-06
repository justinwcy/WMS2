using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllProductByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllProductByCompanyIdQuery, IEnumerable<GetProductResponseDTO>>
    {
        public async Task<IEnumerable<GetProductResponseDTO>> Handle(GetAllProductByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var products = await wmsDbContext.Products
                .AsNoTracking()
                .Where(product => staffIdsInCompany.Contains(product.CreatedBy))
                .Include(product => product.ProductGroups)
                .ToListAsync(cancellationToken);

            var productsDto = products.Adapt<List<GetProductResponseDTO>>();
            foreach (var productDto in productsDto)
            {
                productDto.ProductGroupIds = products
                    .First(product => product.Id == productDto.Id)
                    .ProductGroups
                    .Select(productGroups => productGroups.Id)
                    .ToList();
            }

            return productsDto;
        }
    }
}
    