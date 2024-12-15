using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllCustomerOrderDetailByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllCustomerOrderDetailByCompanyIdQuery, IEnumerable<GetCustomerOrderDetailResponseDTO>>
    {
        public async Task<IEnumerable<GetCustomerOrderDetailResponseDTO>> Handle(GetAllCustomerOrderDetailByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var customerOrderDetails = await wmsDbContext.CustomerOrderDetails
                .AsNoTracking()
                .Where(customerOrderDetail => staffIdsInCompany.Contains(customerOrderDetail.CreatedBy))
                .ToListAsync(cancellationToken);

            var customerOrderDetailsDto = customerOrderDetails.Adapt<List<GetCustomerOrderDetailResponseDTO>>();
            return customerOrderDetailsDto;
        }
    }
}
    