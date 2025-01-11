using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllVendorIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllVendorIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllVendorIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var vendorIds = await wmsDbContext.Vendors
                .AsNoTracking()
                .Where(vendor => staffIdsInCompany.Contains(vendor.CreatedBy))
                .Select(vendor=>vendor.Id)
                .ToListAsync(cancellationToken);

            return vendorIds;
        }
    }
}
    