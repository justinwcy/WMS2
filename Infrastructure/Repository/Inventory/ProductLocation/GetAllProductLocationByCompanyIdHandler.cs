using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetAllProductLocationByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllProductLocationByCompanyIdQuery, IEnumerable<GetProductLocationResponseDTO>>
    {
        public async Task<IEnumerable<GetProductLocationResponseDTO>> Handle(GetAllProductLocationByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var productLocations = await wmsDbContext.ProductLocations
                .AsNoTracking()
                .Where(productLocation => staffIdsInCompany.Contains(productLocation.CreatedBy))
                .ToListAsync(cancellationToken);

            var productLocationsDto = productLocations.Adapt<List<GetProductLocationResponseDTO>>();
            return productLocationsDto;
        }
    }
}
    