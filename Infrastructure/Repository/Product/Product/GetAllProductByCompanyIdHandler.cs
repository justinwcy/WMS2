using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllProductByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllProductByCompanyIdQuery, IEnumerable<GetProductResponseDTO>>
    {
        public async Task<IEnumerable<GetProductResponseDTO>> Handle(GetAllProductByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var products = await wmsDbContext.Products
                .AsNoTracking()
                .Where(product => staffIdsInCompany.Contains(product.CreatedBy))
                .ToListAsync(cancellationToken);

            var productsDto = products.Adapt<List<GetProductResponseDTO>>();
            return productsDto;
        }
    }
}
    