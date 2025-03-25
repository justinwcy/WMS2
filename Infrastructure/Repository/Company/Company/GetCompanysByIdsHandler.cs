using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetCompanysByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetCompaniesByIdsQuery, List<GetCompanyResponseDTO>>
    {
        public async Task<List<GetCompanyResponseDTO>> Handle(GetCompaniesByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var companysFound = await wmsDbContext.Companies.AsNoTracking()
                .Include(company => company.Zones)
                .Include(company => company.Company)
                .Where(company => request.CompanyIds.Contains(company.Id))
                .ToListAsync(cancellationToken);

            var result = new List<GetCompanyResponseDTO>();
            foreach (var companyFound in companysFound)
            {
                var getCompanyResponseDTO = companyFound.Adapt<GetCompanyResponseDTO>();
                getCompanyResponseDTO.ZoneIds = companyFound.Zones.Select(zone => zone.Id).ToList();
                getCompanyResponseDTO.CompanyId = companyFound.Company?.Id;
                result.Add(getCompanyResponseDTO);
            }
            
            return result;
        }
    }
}
