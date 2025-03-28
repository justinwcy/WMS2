using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetVendorByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetVendorByIdQuery, GetVendorResponseDTO>
    {
        public async Task<GetVendorResponseDTO> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var vendorFound = await wmsDbContext.Vendors.AsNoTracking()
                .Include(vendor => vendor.IncomingOrders)
                .FirstAsync(vendor=>vendor.Id == request.Id, cancellationToken);

            var result = vendorFound.Adapt<GetVendorResponseDTO>();
            result.IncomingOrderIds = 
                vendorFound.IncomingOrders?.Select(incomingOrder => incomingOrder.Id).ToList();
            return result;
        }
    }
}
    