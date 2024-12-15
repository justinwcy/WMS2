using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetCustomerOrderDetailByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetCustomerOrderDetailByIdQuery, GetCustomerOrderDetailResponseDTO>
    {
        public async Task<GetCustomerOrderDetailResponseDTO> Handle(GetCustomerOrderDetailByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var customerOrderDetailFound = await wmsDbContext.CustomerOrderDetails.AsNoTracking()
                .FirstAsync(customerOrderDetail=>customerOrderDetail.Id == request.Id, cancellationToken);

            var result = customerOrderDetailFound.Adapt<GetCustomerOrderDetailResponseDTO>();
            return result;
        }
    }
}
    