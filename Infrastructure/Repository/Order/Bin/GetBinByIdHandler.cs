using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetBinByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetBinByIdQuery, GetBinResponseDTO>
    {
        public async Task<GetBinResponseDTO> Handle(GetBinByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var binFound = await wmsDbContext.Bins.AsNoTracking()
                .Include(bin => bin.CustomerOrders)
                .FirstAsync(bin=>bin.Id == request.Id, cancellationToken);

            var result = binFound.Adapt<GetBinResponseDTO>();
            result.CustomerOrderIds = binFound.CustomerOrders?.Select(customerOrder => customerOrder.Id).ToList();

            return result;
        }
    }
}
    