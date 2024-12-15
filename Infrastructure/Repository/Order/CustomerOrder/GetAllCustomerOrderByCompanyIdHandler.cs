using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllCustomerOrderByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllCustomerOrderByCompanyIdQuery, IEnumerable<GetCustomerOrderResponseDTO>>
    {
        public async Task<IEnumerable<GetCustomerOrderResponseDTO>> Handle(GetAllCustomerOrderByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var customerOrders = await wmsDbContext.CustomerOrders
                .AsNoTracking()
                .Where(customerOrder => staffIdsInCompany.Contains(customerOrder.CreatedBy))
                .ToListAsync(cancellationToken);

            var customerOrdersDto = customerOrders.Adapt<List<GetCustomerOrderResponseDTO>>();
            return customerOrdersDto;
        }
    }
}
    