using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllCompanyHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllCompanyQuery, IEnumerable<GetCompanyResponseDTO>>
    {
        public async Task<IEnumerable<GetCompanyResponseDTO>> Handle(GetAllCompanyQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var companies = await wmsDbContext.Companies.AsNoTracking()
                .Include(company=>company.Staffs)
                .Include(company=>company.Warehouses)
                .ToListAsync(cancellationToken: cancellationToken);

            var companiesDto = companies.Adapt<List<GetCompanyResponseDTO>>();
            foreach (var companyDto in companiesDto)
            {
                var matchingCompany = companies.First(company => company.Id == companyDto.Id);
                companyDto.StaffIds = matchingCompany.Staffs.Select(staff => staff.Id).ToList();
                companyDto.WarehouseIds = matchingCompany.Warehouses.Select(warehouse => warehouse.Id).ToList();
            }
            return companiesDto;
        }
    }
}
