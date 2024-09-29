using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteVendorHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteVendorCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundVendor = await wmsDbContext.Vendors.FirstOrDefaultAsync(
                    vendor => vendor.Id == request.Id,
                    cancellationToken);
                if (foundVendor == null)
                {
                    return GeneralDbResponses.ItemNotFound("Vendor");
                }

                wmsDbContext.Vendors.Remove(foundVendor);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("Vendor");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    