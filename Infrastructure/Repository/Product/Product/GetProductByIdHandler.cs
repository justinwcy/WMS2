using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductByIdQuery, GetProductResponseDTO>
    {
        public async Task<GetProductResponseDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productFound = await wmsDbContext.Products.AsNoTracking()
                .Include(product => product.ProductGroups)
                .Include(product => product.Shops).Include(product => product.IncomingOrders)
                .Include(product => product.CurrentInventory).Include(product => product.RefundOrders)
                .Include(product => product.Racks).Include(product => product.CustomerOrderDetails)
                .FirstAsync(product=>product.Id == request.Id, cancellationToken);

            var result = productFound.Adapt<GetProductResponseDTO>();
            result.ProductGroupIds = productFound.ProductGroups.Select(productGroup => productGroup.Id).ToList();
            result.ShopIds = productFound.Shops.Select(shop => shop.Id).ToList();
            result.IncomingOrderIds = productFound.IncomingOrders.Select(incomingOrder => incomingOrder.Id).ToList();
            result.InventoryId = productFound.CurrentInventory?.Id;
            result.RefundOrderIds = productFound.RefundOrders.Select(refundOrder => refundOrder.Id).ToList();
            result.RackIds = productFound.Racks.Select(rack => rack.Id).ToList();
            result.CustomerOrderDetailIds = productFound.CustomerOrderDetails.Select(customerOrderDetail => customerOrderDetail.Id).ToList();
            
            return result;
        }
    }
}
    