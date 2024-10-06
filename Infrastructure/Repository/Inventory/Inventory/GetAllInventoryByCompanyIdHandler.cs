using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetAllInventoryByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllInventoryByCompanyIdQuery, IEnumerable<GetInventoryResponseDTO>>
    {
        public async Task<IEnumerable<GetInventoryResponseDTO>> Handle(GetAllInventoryByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var inventories = await wmsDbContext.Inventories
                .AsNoTracking()
                .Where(inventory => staffIdsInCompany.Contains(inventory.CreatedBy))
                .ToListAsync(cancellationToken);

            var inventoriesDto = inventories.Adapt<List<GetInventoryResponseDTO>>();
            return inventoriesDto;
        }
    }
}
    