using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductGroupByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductGroupByIdQuery, IEnumerable<GetProductGroupResponseDTO>>
    {
        public async Task<IEnumerable<GetProductGroupResponseDTO>> Handle(GetProductGroupByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productGroupFound = wmsDbContext.ProductGroups.AsNoTracking()
                .Where(productGroup=>productGroup.Id == request.Id)
                .ToList();

            var result = productGroupFound.Adapt<IEnumerable<GetProductGroupResponseDTO>>();
            return result;
        }
    }
}
    