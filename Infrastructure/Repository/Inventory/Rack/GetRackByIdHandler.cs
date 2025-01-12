using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetRackByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetRackByIdQuery, GetRackResponseDTO>
    {
        public async Task<GetRackResponseDTO> Handle(GetRackByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var rackFound = await wmsDbContext.Racks.AsNoTracking()
                .Include(rack => rack.Products)
                .FirstAsync(rack=>rack.Id == request.Id, cancellationToken);

            var result = rackFound.Adapt<GetRackResponseDTO>();
            result.ProductIds = rackFound.Products.Select(product=>product.Id).ToList();
            result.ZoneId = rackFound.ZoneId;

            return result;
        }
    }
}
    