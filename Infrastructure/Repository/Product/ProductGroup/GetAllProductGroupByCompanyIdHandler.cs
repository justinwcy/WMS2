using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllProductGroupByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllProductGroupByCompanyIdQuery, IEnumerable<GetProductGroupResponseDTO>>
    {
        public async Task<IEnumerable<GetProductGroupResponseDTO>> Handle(GetAllProductGroupByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var productGroups = await wmsDbContext.ProductGroups
                .AsNoTracking()
                .Where(productGroup => staffIdsInCompany.Contains(productGroup.CreatedBy))
                .Include(productGroup => productGroup.Products)
                .ToListAsync(cancellationToken);

            var productGroupsDto = productGroups.Adapt<List<GetProductGroupResponseDTO>>();
            foreach (var productGroupDto in productGroupsDto)
            {
                var matchingProductGroup = productGroups.First(productGroup => productGroup.Id == productGroupDto.Id);
                productGroupDto.ProductIds = matchingProductGroup.Products.Select(product => product.Id).ToList();
            }

            return productGroupsDto;
        }
    }
}
    