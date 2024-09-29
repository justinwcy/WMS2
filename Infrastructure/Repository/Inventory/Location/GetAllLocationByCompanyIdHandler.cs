using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetAllLocationByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllLocationByCompanyIdQuery, IEnumerable<GetLocationResponseDTO>>
    {
        public async Task<IEnumerable<GetLocationResponseDTO>> Handle(GetAllLocationByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var locations = await wmsDbContext.Locations
                .AsNoTracking()
                .Where(location => staffIdsInCompany.Contains(location.CreatedBy))
                .ToListAsync(cancellationToken);

            var locationsDto = locations.Adapt<List<GetLocationResponseDTO>>();
            return locationsDto;
        }
    }
}
    