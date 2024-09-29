using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetAllVendorByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllVendorByCompanyIdQuery, IEnumerable<GetVendorResponseDTO>>
    {
        public async Task<IEnumerable<GetVendorResponseDTO>> Handle(GetAllVendorByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var vendors = await wmsDbContext.Vendors
                .AsNoTracking()
                .Where(vendor => staffIdsInCompany.Contains(vendor.CreatedBy))
                .ToListAsync(cancellationToken);

            var vendorsDto = vendors.Adapt<List<GetVendorResponseDTO>>();
            return vendorsDto;
        }
    }
}
    