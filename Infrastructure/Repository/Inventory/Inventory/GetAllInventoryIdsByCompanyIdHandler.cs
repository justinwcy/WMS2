using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllInventoryIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllInventoryIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllInventoryIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var inventoryIds = await wmsDbContext.Inventories
                .AsNoTracking()
                .Where(inventory => staffIdsInCompany.Contains(inventory.CreatedBy))
                .Select(inventory => inventory.Id)
                .ToListAsync(cancellationToken);

            return inventoryIds;
        }
    }
}
    