using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

namespace Infrastructure.Repository
{
    public class GetAllStaffIdsByCompanyIdHandler(
        IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllStaffIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllStaffIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(
                wmsDbContext, request.CompanyId, cancellationToken);
            
            return staffIdsInCompany;
        }
    }
}
