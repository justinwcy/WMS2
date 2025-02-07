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
                var vendorFound = await wmsDbContext.Vendors
                    .Include(vendor=>vendor.IncomingOrders)
                    .FirstOrDefaultAsync(vendor => vendor.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (vendorFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Vendor");
                }

                vendorFound.Address = request.Model.Address;
                vendorFound.Email = request.Model.Email;
                vendorFound.FirstName = request.Model.FirstName;
                vendorFound.LastName = request.Model.LastName;
                vendorFound.PhoneNumber = request.Model.PhoneNumber;

                var incomingOrdersToAdd = await wmsDbContext.IncomingOrders
                    .Where(incomingOrder =>
                        request.Model.IncomingOrderIds.Contains(incomingOrder.Id))
                    .ToListAsync(cancellationToken);
                vendorFound.IncomingOrders.RemoveAll(incomingOrder => !request.Model.IncomingOrderIds.Contains(incomingOrder.Id));
                foreach (var incomingOrderToAdd in incomingOrdersToAdd)
                {
                    if (vendorFound.IncomingOrders.All(incomingOrder => incomingOrder.Id != incomingOrderToAdd.Id))
                    {
                        vendorFound.IncomingOrders.Add(incomingOrderToAdd);
                    }
                }
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
    