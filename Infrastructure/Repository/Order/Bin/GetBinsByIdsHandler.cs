using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetBinsByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetBinsByIdsQuery, List<GetBinResponseDTO>>
    {
        public async Task<List<GetBinResponseDTO>> Handle(GetBinsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var binsFound = await wmsDbContext.Bins.AsNoTracking()
                .Where(bin => request.BinIds.Contains(bin.Id))
                .Include(bin => bin.CustomerOrders)
                .ToListAsync(cancellationToken);

            var result = new List<GetBinResponseDTO>();
            foreach (var binFound in binsFound)
            {
                var getBinResponseDTO = binFound.Adapt<GetBinResponseDTO>();
                getBinResponseDTO.CustomerOrderIds = 
                    binFound.CustomerOrders?.Select(customerOrder => customerOrder.Id).ToList();

                result.Add(getBinResponseDTO);
            }
            
            return result;
        }
    }
}
