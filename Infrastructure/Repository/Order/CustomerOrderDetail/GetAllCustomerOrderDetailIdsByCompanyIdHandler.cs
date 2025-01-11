using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllCustomerOrderDetailIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllCustomerOrderDetailIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllCustomerOrderDetailIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var customerOrderDetailIds = await wmsDbContext.CustomerOrderDetails
                .AsNoTracking()
                .Where(customerOrderDetail => staffIdsInCompany.Contains(customerOrderDetail.CreatedBy))
                .Select(customerOrderDetail => customerOrderDetail.Id)
                .ToListAsync(cancellationToken);

            return customerOrderDetailIds;
        }
    }
}
    