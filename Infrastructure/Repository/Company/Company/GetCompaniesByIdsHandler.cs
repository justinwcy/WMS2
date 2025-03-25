using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetCompaniesByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetCompaniesByIdsQuery, List<GetCompanyResponseDTO>>
    {
        public async Task<List<GetCompanyResponseDTO>> Handle(GetCompaniesByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var companiesFound = await wmsDbContext.Companies.AsNoTracking()
                .Where(company => request.CompanyIds.Contains(company.Id))
                .Include(company => company.Staffs)
                .Include(company => company.Warehouses)
                .ToListAsync(cancellationToken);

            var result = new List<GetCompanyResponseDTO>();
            foreach (var companyFound in companiesFound)
            {
                var getCompanyResponseDTO = companyFound.Adapt<GetCompanyResponseDTO>();
                getCompanyResponseDTO.StaffIds = companyFound.Staffs?.Select(staff => staff.Id).ToList();
                getCompanyResponseDTO.WarehouseIds = companyFound.Warehouses?.Select(warehouse => warehouse.Id).ToList();
                result.Add(getCompanyResponseDTO);
            }
            
            return result;
        }
    }
}
