using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateProductHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateProductCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var productFound = await wmsDbContext.Products
                    .Include(product=>product.CustomerOrderDetails)
                    .Include(product=>product.IncomingOrders)
                    .Include(product=>product.ProductGroups)
                    .Include(product=>product.RefundOrders)
                    .Include(product=>product.Racks)
                    .Include(product=>product.Shops)
                    .FirstOrDefaultAsync(product => product.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (productFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Product");
                }

                productFound.Name = request.Model.Name;
                productFound.Description = request.Model.Description;
                productFound.Height = request.Model.Height;
                productFound.Length = request.Model.Length;
                productFound.Price = request.Model.Price;
                productFound.Sku = request.Model.Sku;
                productFound.Tag = request.Model.Tag;
                productFound.Width = request.Model.Width;
                productFound.Weight = request.Model.Weight;
                
                var customerOrderDetailsToAdd = await wmsDbContext.CustomerOrderDetails
                    .Where(customerOrderDetail => request.Model.CustomerOrderDetailIds.Contains(customerOrderDetail.Id))
                    .ToListAsync(cancellationToken);
                productFound.CustomerOrderDetails.RemoveAll(product => !request.Model.CustomerOrderDetailIds.Contains(product.Id));
                foreach (var customerOrderDetailToAdd in customerOrderDetailsToAdd)
                {
                    if (productFound.CustomerOrderDetails.All(customerOrderDetail => customerOrderDetail.Id != customerOrderDetailToAdd.Id))
                    {
                        productFound.CustomerOrderDetails.Add(customerOrderDetailToAdd);
                    }
                }

                var incomingOrdersToAdd = await wmsDbContext.IncomingOrders
                    .Where(incomingOrder => request.Model.IncomingOrderIds.Contains(incomingOrder.Id))
                    .ToListAsync(cancellationToken);
                productFound.IncomingOrders.RemoveAll(incomingOrder => !request.Model.IncomingOrderIds.Contains(incomingOrder.Id));
                foreach (var incomingOrderToAdd in incomingOrdersToAdd)
                {
                    if (productFound.IncomingOrders.All(incomingOrder => incomingOrder.Id != incomingOrderToAdd.Id))
                    {
                        productFound.IncomingOrders.Add(incomingOrderToAdd);
                    }
                }

                var refundOrdersToAdd = await wmsDbContext.RefundOrders
                    .Where(refundOrder => request.Model.RefundOrderIds.Contains(refundOrder.Id))
                    .ToListAsync(cancellationToken);
                productFound.RefundOrders.RemoveAll(refundOrder => !request.Model.RefundOrderIds.Contains(refundOrder.Id));
                foreach (var refundOrderToAdd in refundOrdersToAdd)
                {
                    if (productFound.RefundOrders.All(refundOrder => refundOrder.Id != refundOrderToAdd.Id))
                    {
                        productFound.RefundOrders.Add(refundOrderToAdd);
                    }
                }

                var productGroupsToAdd = await wmsDbContext.ProductGroups
                    .Where(productGroup => request.Model.ProductGroupIds.Contains(productGroup.Id))
                    .ToListAsync(cancellationToken);
                productFound.ProductGroups.RemoveAll(productGroup => !request.Model.ProductGroupIds.Contains(productGroup.Id));
                foreach (var productGroupToAdd in productGroupsToAdd)
                {
                    if (productFound.ProductGroups.All(productGroup => productGroup.Id != productGroupToAdd.Id))
                    {
                        productFound.ProductGroups.Add(productGroupToAdd);
                    }
                }

                var racksToAdd = await wmsDbContext.Racks
                    .Where(rack => request.Model.RackIds.Contains(rack.Id))
                    .ToListAsync(cancellationToken);
                productFound.Racks.RemoveAll(rack => !request.Model.RackIds.Contains(rack.Id));
                foreach (var rackToAdd in racksToAdd)
                {
                    if (productFound.Racks.All(rack => rack.Id != rackToAdd.Id))
                    {
                        productFound.Racks.Add(rackToAdd);
                    }
                }

                var shopsToAdd = await wmsDbContext.Shops
                    .Where(shop => request.Model.ShopIds.Contains(shop.Id))
                    .ToListAsync(cancellationToken);
                productFound.Shops.RemoveAll(shop => !request.Model.ShopIds.Contains(shop.Id));
                foreach (var shopToAdd in shopsToAdd)
                {
                    if (productFound.Shops.All(shop => shop.Id != shopToAdd.Id))
                    {
                        productFound.Shops.Add(shopToAdd);
                    }
                }

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Product");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    