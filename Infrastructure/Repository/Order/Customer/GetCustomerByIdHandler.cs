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
                .FirstAsync(customer=>customer.Id == request.Id, cancellationToken);

            var result = customerFound.Adapt<GetCustomerResponseDTO>();
            return result;
        }
    }
}
    