using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetAllBinByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllBinByCompanyIdQuery, IEnumerable<GetBinResponseDTO>>
    {
        public async Task<IEnumerable<GetBinResponseDTO>> Handle(GetAllBinByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var bins = await wmsDbContext.Bins
                .AsNoTracking()
                .Where(bin => staffIdsInCompany.Contains(bin.CreatedBy))
                .ToListAsync(cancellationToken);

            var binsDto = bins.Adapt<List<GetBinResponseDTO>>();
            return binsDto;
        }
    }
}
    