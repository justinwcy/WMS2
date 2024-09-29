using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetProductLocationByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductLocationByIdQuery, GetProductLocationResponseDTO>
    {
        public async Task<GetProductLocationResponseDTO> Handle(GetProductLocationByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productLocationFound = await wmsDbContext.ProductLocations.AsNoTracking()
                .FirstAsync(productLocation=>productLocation.Id == request.Id, cancellationToken);

            var result = productLocationFound.Adapt<GetProductLocationResponseDTO>();
            return result;
        }
    }
}
    