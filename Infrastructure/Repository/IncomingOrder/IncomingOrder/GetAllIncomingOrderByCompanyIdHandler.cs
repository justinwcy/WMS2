using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllIncomingOrderByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllIncomingOrderByCompanyIdQuery, IEnumerable<GetIncomingOrderResponseDTO>>
    {
        public async Task<IEnumerable<GetIncomingOrderResponseDTO>> Handle(GetAllIncomingOrderByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var incomingOrders = await wmsDbContext.IncomingOrders
                .AsNoTracking()
                .Where(incomingOrder => staffIdsInCompany.Contains(incomingOrder.CreatedBy))
                .ToListAsync(cancellationToken);

            var incomingOrdersDto = incomingOrders.Adapt<List<GetIncomingOrderResponseDTO>>();
            return incomingOrdersDto;
        }
    }
}
    