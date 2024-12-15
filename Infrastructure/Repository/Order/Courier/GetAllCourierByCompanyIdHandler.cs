using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllCourierByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllCourierByCompanyIdQuery, IEnumerable<GetCourierResponseDTO>>
    {
        public async Task<IEnumerable<GetCourierResponseDTO>> Handle(GetAllCourierByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var couriers = await wmsDbContext.Couriers
                .AsNoTracking()
                .Where(courier => staffIdsInCompany.Contains(courier.CreatedBy))
                .ToListAsync(cancellationToken);

            var couriersDto = couriers.Adapt<List<GetCourierResponseDTO>>();
            return couriersDto;
        }
    }
}
    