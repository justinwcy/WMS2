using Application.DTO.Response;
using Application.Service.Queries;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetCompanyByIdHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetCompanyByIdQuery, GetCompanyResponseDTO>
    {
        public async Task<GetCompanyResponseDTO> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var companyFound = await wmsDbContext.Companies.AsNoTracking()
                .FirstAsync(company=>company.Id == request.Id, cancellationToken);

            var result = companyFound.Adapt<GetCompanyResponseDTO>();
            return result;
        }
    }
}
