using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllCompanyIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllCompanyIdsQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllCompanyIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var companyIds = await wmsDbContext.Companies.AsNoTracking()
                .Select(company=>company.Id)
                .ToListAsync(cancellationToken: cancellationToken);

            return companyIds;
        }
    }
}
