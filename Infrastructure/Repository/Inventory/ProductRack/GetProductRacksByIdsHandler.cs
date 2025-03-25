using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductRacksByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductRacksByIdsQuery, List<GetProductRackResponseDTO>>
    {
        public async Task<List<GetProductRackResponseDTO>> Handle(GetProductRacksByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productRacksFound = await wmsDbContext.ProductRacks.AsNoTracking()
                .Include(productRack => productRack.Zones)
                .Include(productRack => productRack.Company)
                .Where(productRack => request.ProductRackIds.Contains(productRack.Id))
                .ToListAsync(cancellationToken);

            var result = new List<GetProductRackResponseDTO>();
            foreach (var productRackFound in productRacksFound)
            {
                var getProductRackResponseDTO = productRackFound.Adapt<GetProductRackResponseDTO>();
                getProductRackResponseDTO.ZoneIds = productRackFound.Zones.Select(zone => zone.Id).ToList();
                getProductRackResponseDTO.CompanyId = productRackFound.Company?.Id;
                result.Add(getProductRackResponseDTO);
            }
            
            return result;
        }
    }
}
