using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductByIdQuery, GetProductResponseDTO>
    {
        public async Task<GetProductResponseDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productFound = await wmsDbContext.Products.AsNoTracking()
                .FirstAsync(product=>product.Id == request.Id, cancellationToken);

            var result = productFound.Adapt<GetProductResponseDTO>();
            return result;
        }
    }
}
    