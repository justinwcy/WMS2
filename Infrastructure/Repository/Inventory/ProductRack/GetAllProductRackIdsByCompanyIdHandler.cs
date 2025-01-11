using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllProductRackIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllProductRackIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllProductRackIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var productRackIds = await wmsDbContext.ProductRacks
                .AsNoTracking()
                .Where(productRack => staffIdsInCompany.Contains(productRack.CreatedBy))
                .Select(productRack=>productRack.Id)
                .ToListAsync(cancellationToken);

            return productRackIds;
        }
    }
}
    