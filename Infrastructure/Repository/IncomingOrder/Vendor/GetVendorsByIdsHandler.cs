using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetVendorsByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetVendorsByIdsQuery, List<GetVendorResponseDTO>>
    {
        public async Task<List<GetVendorResponseDTO>> Handle(GetVendorsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var vendorsFound = await wmsDbContext.Vendors.AsNoTracking()
                .Where(vendor => request.VendorIds.Contains(vendor.Id))
                .Include(vendor => vendor.IncomingOrders)
                .ToListAsync(cancellationToken);

            var result = new List<GetVendorResponseDTO>();
            foreach (var vendorFound in vendorsFound)
            {
                var getVendorResponseDTO = vendorFound.Adapt<GetVendorResponseDTO>();
                getVendorResponseDTO.IncomingOrderIds =
                    vendorFound.IncomingOrders?.Select(incomingOrder => incomingOrder.Id).ToList();
                result.Add(getVendorResponseDTO);
            }
            
            return result;
        }
    }
}
