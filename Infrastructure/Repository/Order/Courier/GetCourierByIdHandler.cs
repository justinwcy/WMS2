using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetCourierByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetCourierByIdQuery, GetCourierResponseDTO>
    {
        public async Task<GetCourierResponseDTO> Handle(GetCourierByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var courierFound = await wmsDbContext.Couriers.AsNoTracking()
                .Include(courier => courier.CustomerOrders)
                .FirstAsync(courier=>courier.Id == request.Id, cancellationToken);

            var result = courierFound.Adapt<GetCourierResponseDTO>();
            result.CustomerOrderIds = courierFound.CustomerOrders.Select(customerOrder => customerOrder.Id).ToList();
            
            return result;
        }
    }
}
    