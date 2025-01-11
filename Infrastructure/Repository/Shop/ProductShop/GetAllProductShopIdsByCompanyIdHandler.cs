using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllProductShopIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllProductShopIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllProductShopIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var productShopIds = await wmsDbContext.ProductShops
                .AsNoTracking()
                .Where(productShop => staffIdsInCompany.Contains(productShop.CreatedBy))
                .Select(productShop=>productShop.Id)
                .ToListAsync(cancellationToken);

            return productShopIds;
        }
    }
}
    