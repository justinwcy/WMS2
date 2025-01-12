using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductRackByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductRackByIdQuery, GetProductRackResponseDTO>
    {
        public async Task<GetProductRackResponseDTO> Handle(GetProductRackByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productRackFound = await wmsDbContext.ProductRacks.AsNoTracking()
                .FirstAsync(productRack => productRack.Id == request.Id, cancellationToken);

            var result = productRackFound.Adapt<GetProductRackResponseDTO>();
            result.ProductId = productRackFound.ProductId;
            result.RackId = productRackFound.RackId;
            
            return result;
        }
    }
}
    