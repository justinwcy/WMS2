using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllIncomingOrderProductIdsByIncomingOrderIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllIncomingOrderProductIdsByIncomingOrderIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllIncomingOrderProductIdsByIncomingOrderIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var incomingOrderProductIds = await wmsDbContext.IncomingOrderProducts
                .AsNoTracking()
                .Where(incomingOrderProduct => incomingOrderProduct.ProductId == request.IncomingOrderId)
                .Select(incomingOrderProduct=>incomingOrderProduct.Id)
                .ToListAsync(cancellationToken);
            return incomingOrderProductIds;
        }
    }
}
    