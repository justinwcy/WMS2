using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllCustomerByCompanyIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllCustomerByCompanyIdQuery, IEnumerable<GetCustomerResponseDTO>>
    {
        public async Task<IEnumerable<GetCustomerResponseDTO>> Handle(GetAllCustomerByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var staffIdsInCompany = await Utilities.GetStaffIdsInsideCompany(wmsDbContext, request.CompanyId, cancellationToken);

            var customers = await wmsDbContext.Customers
                .AsNoTracking()
                .Where(customer => staffIdsInCompany.Contains(customer.CreatedBy))
                .ToListAsync(cancellationToken);

            var customersDto = customers.Adapt<List<GetCustomerResponseDTO>>();
            return customersDto;
        }
    }
}
    