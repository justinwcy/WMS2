using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetAllRefundOrderProductByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllRefundOrderProductByCompanyIdQuery, IEnumerable<GetRefundOrderProductResponseDTO>>
    {
        public async Task<IEnumerable<GetRefundOrderProductResponseDTO>> Handle(GetAllRefundOrderProductByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var refundOrderProducts = await wmsDbContext.RefundOrderProducts
                .AsNoTracking()
                .Where(refundOrderProduct => staffIdsInCompany.Contains(refundOrderProduct.CreatedBy))
                .ToListAsync(cancellationToken);

            var refundOrderProductsDto = refundOrderProducts.Adapt<List<GetRefundOrderProductResponseDTO>>();
            return refundOrderProductsDto;
        }
    }
}
    