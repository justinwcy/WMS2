using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetLocationByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetLocationByIdQuery, GetLocationResponseDTO>
    {
        public async Task<GetLocationResponseDTO> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var locationFound = await wmsDbContext.Locations.AsNoTracking()
                .FirstAsync(location=>location.Id == request.Id, cancellationToken);

            var result = locationFound.Adapt<GetLocationResponseDTO>();
            return result;
        }
    }
}
    