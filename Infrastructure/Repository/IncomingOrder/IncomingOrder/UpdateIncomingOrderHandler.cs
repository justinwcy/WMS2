using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateIncomingOrderHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateIncomingOrderCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateIncomingOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var incomingOrderFound = await wmsDbContext.IncomingOrders.FirstOrDefaultAsync(
                    incomingOrder => incomingOrder.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (incomingOrderFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("IncomingOrder");
                }

                incomingOrderFound.Status = request.Model.Status;
                incomingOrderFound.IncomingDate = request.Model.IncomingDate;
                incomingOrderFound.PONumber = request.Model.PONumber;
                incomingOrderFound.ReceivingDate = request.Model.ReceivingDate;

                var vendorFound = await wmsDbContext.Vendors.FirstOrDefaultAsync(
                    vendor => vendor.Id == request.Model.VendorId, 
                    cancellationToken);
                if (vendorFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Vendor");
                }
                incomingOrderFound.Vendor = vendorFound;
                incomingOrderFound.VendorId = request.Model.VendorId;

                var incomingOrderProductsToAdd = await wmsDbContext.IncomingOrderProducts
                    .Where(incomingOrderProduct =>
                        request.Model.IncomingOrderProductIds.Contains(incomingOrderProduct.Id))
                    .Include(incomingOrderProduct => incomingOrderProduct.Product)
                    .ToListAsync(cancellationToken);
                incomingOrderFound.IncomingOrderProducts.RemoveAll(incomingOrderProduct => !request.Model.IncomingOrderProductIds.Contains(incomingOrderProduct.Id));
                foreach (var incomingOrderProductToAdd in incomingOrderProductsToAdd)
                {
                    if (incomingOrderFound.IncomingOrderProducts.All(incomingOrderProduct => incomingOrderProduct.Id != incomingOrderProductToAdd.Id))
                    {
                        incomingOrderFound.IncomingOrderProducts.Add(incomingOrderProductToAdd);
                    }
                }

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("IncomingOrder");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    