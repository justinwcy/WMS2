using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetProductsByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetProductsByIdsQuery, List<GetProductResponseDTO>>
    {
        public async Task<List<GetProductResponseDTO>> Handle(GetProductsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var productsFound = await wmsDbContext.Products.AsNoTracking()
                .Where(product => request.ProductIds.Contains(product.Id))
                .Include(product => product.ProductGroups)
                .Include(product => product.Shops)
                .Include(product => product.IncomingOrders)
                .Include(product => product.Racks)
                .Include(product => product.CustomerOrderDetails)
                .Include(product => product.RefundOrders)
                .ToListAsync(cancellationToken);

            var result = new List<GetProductResponseDTO>();
            foreach (var productFound in productsFound)
            {
                var getProductResponseDTO = productFound.Adapt<GetProductResponseDTO>();
                getProductResponseDTO.ProductGroupIds = productFound.ProductGroups.Select(productGroup => productGroup.Id).ToList();
                getProductResponseDTO.ShopIds = productFound.Shops.Select(shop => shop.Id).ToList();
                getProductResponseDTO.IncomingOrderIds = productFound.IncomingOrders.Select(incomingOrder => incomingOrder.Id).ToList();
                getProductResponseDTO.RefundOrderIds = productFound.RefundOrders.Select(refundOrder => refundOrder.Id).ToList();
                getProductResponseDTO.RackIds = productFound.Racks.Select(rack => rack.Id).ToList();
                getProductResponseDTO.CustomerOrderDetailIds = productFound.CustomerOrderDetails.Select(customerOrderDetail => customerOrderDetail.Id).ToList();
                result.Add(getProductResponseDTO);
            }
            
            return result;
        }
    }
}
