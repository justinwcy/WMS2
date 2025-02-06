using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetIncomingOrderByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetIncomingOrderByIdQuery, GetIncomingOrderResponseDTO>
    {
        public async Task<GetIncomingOrderResponseDTO> Handle(GetIncomingOrderByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var incomingOrderFound = await wmsDbContext.IncomingOrders.AsNoTracking()
                .Include(incomingOrder => incomingOrder.IncomingOrderProducts)
                .FirstAsync(incomingOrder=>incomingOrder.Id == request.Id, cancellationToken);

            var result = incomingOrderFound.Adapt<GetIncomingOrderResponseDTO>();
            result.VendorId = incomingOrderFound.VendorId;
            result.IncomingOrderProductIds = incomingOrderFound.IncomingOrderProducts
                .Select(incomingOrderProduct => incomingOrderProduct.Id)
                .ToList();
            return result;
        }
    }
}
    