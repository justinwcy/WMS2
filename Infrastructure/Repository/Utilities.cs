using Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public static class Utilities
    {
        public static async Task<List<Guid>> GetStaffIdsInsideCompany(
            WmsDbContext wmsDbContext, Guid companyId, CancellationToken cancellationToken)
        {
            var companyFound = await wmsDbContext.Companies.AsNoTracking()
                .Include(company => company.Staffs)
                .FirstOrDefaultAsync(company => company.Id == companyId,
                    cancellationToken);
            if (companyFound == null)
            {
                throw new Exception($"Invalid company Id = {companyId}");
            }
            var staffIdsInCompany = companyFound.Staffs.Select(staff => staff.Id).ToList();
            return staffIdsInCompany;
        }

    }
}
