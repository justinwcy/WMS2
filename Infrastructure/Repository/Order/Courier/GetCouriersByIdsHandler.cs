using Application.DTO.Response;
using Application.Service.Queries;
using Infrastructure.Data;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetCouriersByIdsHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetCouriersByIdsQuery, List<GetCourierResponseDTO>>
    {
        public async Task<List<GetCourierResponseDTO>> Handle(GetCouriersByIdsQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var couriersFound = await wmsDbContext.Couriers.AsNoTracking()
                .Where(courier => request.CourierIds.Contains(courier.Id))
                .Include(courier => courier.CustomerOrders)
                .ToListAsync(cancellationToken);

            var result = new List<GetCourierResponseDTO>();
            foreach (var courierFound in couriersFound)
            {
                var getCourierResponseDTO = courierFound.Adapt<GetCourierResponseDTO>();
                getCourierResponseDTO.CustomerOrderIds = courierFound.CustomerOrders?
                    .Select(customerOrder => customerOrder.Id).ToList();
                result.Add(getCourierResponseDTO);
            }
            
            return result;
        }
    }
}
