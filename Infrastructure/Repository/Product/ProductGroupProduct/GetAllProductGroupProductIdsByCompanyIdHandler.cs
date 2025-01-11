using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllProductGroupProductIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllProductGroupProductIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllProductGroupProductIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var productGroupProducts = await wmsDbContext.ProductGroupProducts
                .AsNoTracking()
                .Where(productGroupProduct => staffIdsInCompany.Contains(productGroupProduct.CreatedBy))
                .Select(productGroupProduct => productGroupProduct.Id)
                .ToListAsync(cancellationToken);

            return productGroupProducts;
        }
    }
}
    