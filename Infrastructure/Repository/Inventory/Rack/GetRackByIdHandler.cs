using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetRackByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetRackByIdQuery, GetRackResponseDTO>
    {
        public async Task<GetRackResponseDTO> Handle(GetRackByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var rackFound = await wmsDbContext.Racks.AsNoTracking()
                .FirstAsync(rack=>rack.Id == request.Id, cancellationToken);

            var result = rackFound.Adapt<GetRackResponseDTO>();
            return result;
        }
    }
}
    