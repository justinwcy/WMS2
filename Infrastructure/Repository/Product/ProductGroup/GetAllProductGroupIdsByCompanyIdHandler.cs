using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllProductGroupIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllProductGroupIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllProductGroupIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var productGroupIds = await wmsDbContext.ProductGroups
                .AsNoTracking()
                .Where(productGroup => staffIdsInCompany.Contains(productGroup.CreatedBy))
                .Select(productGroup=>productGroup.Id)
                .ToListAsync(cancellationToken);

            return productGroupIds;
        }
    }
}
    