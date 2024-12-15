using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllProductSkuByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllProductSkuByCompanyIdQuery, IEnumerable<GetProductSkuResponseDTO>>
    {
        public async Task<IEnumerable<GetProductSkuResponseDTO>> Handle(GetAllProductSkuByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var productSkus = await wmsDbContext.ProductSkus
                .AsNoTracking()
                .Where(productSku => staffIdsInCompany.Contains(productSku.CreatedBy))
                .ToListAsync(cancellationToken);

            var productSkusDto = productSkus.Adapt<List<GetProductSkuResponseDTO>>();
            return productSkusDto;
        }
    }
}
    