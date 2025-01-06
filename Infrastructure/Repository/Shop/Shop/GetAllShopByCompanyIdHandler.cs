using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllShopByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllShopByCompanyIdQuery, IEnumerable<GetShopResponseDTO>>
    {
        public async Task<IEnumerable<GetShopResponseDTO>> Handle(GetAllShopByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var shops = await wmsDbContext.Shops
                .AsNoTracking()
                .Where(shop => staffIdsInCompany.Contains(shop.CreatedBy))
                .Include(shop => shop.Products)
                .ToListAsync(cancellationToken);

            var shopsDto = shops.Adapt<List<GetShopResponseDTO>>();
            foreach (var shopDto in shopsDto)
            {
                shopDto.ProductIds = shops
                    .First(shop => shop.Id == shopDto.Id)
                    .Products
                    .Select(product => product.Id)
                    .ToList();
            }
            return shopsDto;
        }
    }
}
    