using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllRackByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllRackByCompanyIdQuery, IEnumerable<GetRackResponseDTO>>
    {
        public async Task<IEnumerable<GetRackResponseDTO>> Handle(GetAllRackByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var racks = await wmsDbContext.Racks
                .AsNoTracking()
                .Where(rack => staffIdsInCompany.Contains(rack.CreatedBy))
                .ToListAsync(cancellationToken);

            var racksDto = racks.Adapt<List<GetRackResponseDTO>>();
            return racksDto;
        }
    }
}
    