using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllWarehouseIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllWarehouseIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllWarehouseIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var warehouseIds = await wmsDbContext.Warehouses
                .AsNoTracking()
                .Where(warehouse => staffIdsInCompany.Contains(warehouse.CreatedBy))
                .Select(warehouse => warehouse.Id)
                .ToListAsync(cancellationToken);

            return warehouseIds;
        }
    }
}
    