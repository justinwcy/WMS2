using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllIncomingOrderProductByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllIncomingOrderProductByCompanyIdQuery, IEnumerable<GetIncomingOrderProductResponseDTO>>
    {
        public async Task<IEnumerable<GetIncomingOrderProductResponseDTO>> Handle(GetAllIncomingOrderProductByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var incomingOrderProducts = await wmsDbContext.IncomingOrderProducts
                .AsNoTracking()
                .Where(incomingOrderProduct => staffIdsInCompany.Contains(incomingOrderProduct.CreatedBy))
                .ToListAsync(cancellationToken);

            var incomingOrderProductsDto = incomingOrderProducts.Adapt<List<GetIncomingOrderProductResponseDTO>>();
            return incomingOrderProductsDto;
        }
    }
}
    