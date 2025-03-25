using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetCustomerByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetCustomerByIdQuery, GetCustomerResponseDTO>
    {
        public async Task<GetCustomerResponseDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var customerFound = await wmsDbContext.Customers.AsNoTracking()
                .Include(customer => customer.CustomerOrders)
                .FirstAsync(customer=>customer.Id == request.Id, cancellationToken);

            var result = customerFound.Adapt<GetCustomerResponseDTO>();
            result.CustomerOrderIds = customerFound.CustomerOrders?
                .Select(customerOrder => customerOrder.Id).ToList();
            return result;
        }
    }
}
    