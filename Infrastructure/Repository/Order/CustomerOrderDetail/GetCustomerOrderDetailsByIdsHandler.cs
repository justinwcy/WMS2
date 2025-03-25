using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetCustomerOrderDetailsByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetCustomerOrderDetailsByIdsQuery, List<GetCustomerOrderDetailResponseDTO>>
    {
        public async Task<List<GetCustomerOrderDetailResponseDTO>> Handle(GetCustomerOrderDetailsByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var customerOrderDetailsFound = await wmsDbContext.CustomerOrderDetails.AsNoTracking()
                .Where(customerOrderDetail => request.CustomerOrderDetailIds.Contains(customerOrderDetail.Id))
                .ToListAsync(cancellationToken);

            var result = new List<GetCustomerOrderDetailResponseDTO>();
            foreach (var customerOrderDetailFound in customerOrderDetailsFound)
            {
                var getCustomerOrderDetailResponseDTO = customerOrderDetailFound.Adapt<GetCustomerOrderDetailResponseDTO>();
                getCustomerOrderDetailResponseDTO.ProductId = customerOrderDetailFound.ProductId;
                getCustomerOrderDetailResponseDTO.CustomerOrderId = customerOrderDetailFound.CustomerOrderId;
                result.Add(getCustomerOrderDetailResponseDTO);
            }
            
            return result;
        }
    }
}
