using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateVendorHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateVendorCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var vendorFound = await wmsDbContext.Vendors.FirstOrDefaultAsync(
                    vendor => vendor.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (vendorFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Vendor");
                }

                wmsDbContext.Entry(vendorFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<Vendor>();
                wmsDbContext.Vendors.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Vendor");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    