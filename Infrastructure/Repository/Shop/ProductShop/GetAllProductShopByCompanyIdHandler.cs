using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetAllProductShopByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllProductShopByCompanyIdQuery, IEnumerable<GetProductShopResponseDTO>>
    {
        public async Task<IEnumerable<GetProductShopResponseDTO>> Handle(GetAllProductShopByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var productShops = await wmsDbContext.ProductShops
                .AsNoTracking()
                .Where(productShop => staffIdsInCompany.Contains(productShop.CreatedBy))
                .ToListAsync(cancellationToken);

            var productShopsDto = productShops.Adapt<List<GetProductShopResponseDTO>>();
            return productShopsDto;
        }
    }
}
    