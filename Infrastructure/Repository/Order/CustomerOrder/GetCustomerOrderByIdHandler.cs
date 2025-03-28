using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetCustomerOrderByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetCustomerOrderByIdQuery, GetCustomerOrderResponseDTO>
    {
        public async Task<GetCustomerOrderResponseDTO> Handle(GetCustomerOrderByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var customerOrderFound = await wmsDbContext.CustomerOrders.AsNoTracking()
                .Include(customerOrder => customerOrder.CustomerOrderDetails)
                .FirstAsync(customerOrder=>customerOrder.Id == request.Id, cancellationToken);

            var result = customerOrderFound.Adapt<GetCustomerOrderResponseDTO>();
            result.BinId = customerOrderFound.BinId;
            result.CourierId = customerOrderFound.CourierId;
            result.CustomerId = customerOrderFound.CustomerId;
            result.CustomerOrderDetailIds = customerOrderFound.CustomerOrderDetails?
                .Select(customerOrderDetail => customerOrderDetail.Id).ToList();
            return result;
        }
    }
}
    