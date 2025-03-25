using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetCustomerOrdersByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetCustomerOrdersByIdsQuery, List<GetCustomerOrderResponseDTO>>
    {
        public async Task<List<GetCustomerOrderResponseDTO>> Handle(GetCustomerOrdersByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var customerOrdersFound = await wmsDbContext.CustomerOrders.AsNoTracking()
                .Where(customerOrder => request.CustomerOrderIds.Contains(customerOrder.Id))
                .Include(customerOrder => customerOrder.CustomerOrderDetails)

                .ToListAsync(cancellationToken);

            var result = new List<GetCustomerOrderResponseDTO>();
            foreach (var customerOrderFound in customerOrdersFound)
            {
                var getCustomerOrderResponseDTO = customerOrderFound.Adapt<GetCustomerOrderResponseDTO>();
                getCustomerOrderResponseDTO.BinId = customerOrderFound.BinId;
                getCustomerOrderResponseDTO.CourierId = customerOrderFound.CourierId;
                getCustomerOrderResponseDTO.CustomerId = customerOrderFound.CustomerId;
                getCustomerOrderResponseDTO.CustomerOrderDetailIds = customerOrderFound.CustomerOrderDetails?
                    .Select(customerOrderDetail => customerOrderDetail.Id).ToList();
                result.Add(getCustomerOrderResponseDTO);
            }
            
            return result;
        }
    }
}
