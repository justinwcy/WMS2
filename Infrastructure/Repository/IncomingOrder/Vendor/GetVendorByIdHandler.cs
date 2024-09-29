using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Orders.Handlers
{
    public class GetVendorByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetVendorByIdQuery, GetVendorResponseDTO>
    {
        public async Task<GetVendorResponseDTO> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var vendorFound = await wmsDbContext.Vendors.AsNoTracking()
                .FirstAsync(vendor=>vendor.Id == request.Id, cancellationToken);

            var result = vendorFound.Adapt<GetVendorResponseDTO>();
            return result;
        }
    }
}
    