using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetCustomersByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetCustomersByIdsQuery, List<GetCustomerResponseDTO>>
    {
        public async Task<List<GetCustomerResponseDTO>> Handle(GetCustomersByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var customersFound = await wmsDbContext.Customers.AsNoTracking()
                .Where(customer => request.CustomerIds.Contains(customer.Id))
                .Include(customer => customer.CustomerOrders)
                .ToListAsync(cancellationToken);

            var result = new List<GetCustomerResponseDTO>();
            foreach (var customerFound in customersFound)
            {
                var getCustomerResponseDTO = customerFound.Adapt<GetCustomerResponseDTO>();
                getCustomerResponseDTO.CustomerOrderIds = customerFound.CustomerOrders?
                    .Select(customerOrder => customerOrder.Id).ToList();
                result.Add(getCustomerResponseDTO);
            }
            
            return result;
        }
    }
}
