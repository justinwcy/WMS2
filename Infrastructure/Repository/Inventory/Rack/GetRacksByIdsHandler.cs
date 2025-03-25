using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetRacksByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetRacksByIdsQuery, List<GetRackResponseDTO>>
    {
        public async Task<List<GetRackResponseDTO>> Handle(GetRacksByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var racksFound = await wmsDbContext.Racks.AsNoTracking()
                .Where(rack => request.RackIds.Contains(rack.Id))
                .Include(rack => rack.Products)
                .ToListAsync(cancellationToken);

            var result = new List<GetRackResponseDTO>();
            foreach (var rackFound in racksFound)
            {
                var getRackResponseDTO = rackFound.Adapt<GetRackResponseDTO>();
                getRackResponseDTO.ProductIds = rackFound.Products?.Select(product => product.Id).ToList();
                getRackResponseDTO.ZoneId = rackFound.ZoneId;
                result.Add(getRackResponseDTO);
            }
            
            return result;
        }
    }
}
