using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetProductRackByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductRackByIdQuery, GetProductRackResponseDTO>
    {
        public async Task<GetProductRackResponseDTO> Handle(GetProductRackByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var ProductRackFound = await wmsDbContext.ProductRacks.AsNoTracking()
                .FirstAsync(ProductRack=>ProductRack.Id == request.Id, cancellationToken);

            var result = ProductRackFound.Adapt<GetProductRackResponseDTO>();
            return result;
        }
    }
}
    