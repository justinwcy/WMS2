using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllCustomerOrderIdsByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllCustomerOrderIdsByCompanyIdQuery, List<Guid>>
    {
        public async Task<List<Guid>> Handle(GetAllCustomerOrderIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var customerOrderIds = await wmsDbContext.CustomerOrders
                .AsNoTracking()
                .Where(customerOrder => staffIdsInCompany.Contains(customerOrder.CreatedBy))
                .Select(customerOrder => customerOrder.Id)
                .ToListAsync(cancellationToken);

            return customerOrderIds;
        }
    }
}
    