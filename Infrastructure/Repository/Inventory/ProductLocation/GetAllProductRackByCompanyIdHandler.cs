using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetAllProductRackByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllProductRackByCompanyIdQuery, IEnumerable<GetProductRackResponseDTO>>
    {
        public async Task<IEnumerable<GetProductRackResponseDTO>> Handle(GetAllProductRackByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var ProductRacks = await wmsDbContext.ProductRacks
                .AsNoTracking()
                .Where(ProductRack => staffIdsInCompany.Contains(ProductRack.CreatedBy))
                .ToListAsync(cancellationToken);

            var ProductRacksDto = ProductRacks.Adapt<List<GetProductRackResponseDTO>>();
            return ProductRacksDto;
        }
    }
}
    