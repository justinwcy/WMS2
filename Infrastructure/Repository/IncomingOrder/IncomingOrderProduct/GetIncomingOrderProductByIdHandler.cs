using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetIncomingOrderProductByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetIncomingOrderProductByIdQuery, GetIncomingOrderProductResponseDTO>
    {
        public async Task<GetIncomingOrderProductResponseDTO> Handle(GetIncomingOrderProductByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var incomingOrderProductFound = await wmsDbContext.IncomingOrderProducts.AsNoTracking()
                .FirstAsync(incomingOrderProduct=>incomingOrderProduct.Id == request.Id, cancellationToken);

            var result = incomingOrderProductFound.Adapt<GetIncomingOrderProductResponseDTO>();
            return result;
        }
    }
}
    