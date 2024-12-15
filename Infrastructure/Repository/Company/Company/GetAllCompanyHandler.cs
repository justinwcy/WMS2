using Application.DTO.Response;
using Application.Service.Queries;

using Domain.Entities;

using Infrastructure.Data;
using Mapster;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetAllCompanyHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<GetAllCompanyQuery, IEnumerable<GetCompanyResponseDTO>>
    {
        public async Task<IEnumerable<GetCompanyResponseDTO>> Handle(GetAllCompanyQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var companies = await wmsDbContext.Companies.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

            var companiesDto = companies.Adapt<List<GetCompanyResponseDTO>>();
            return companiesDto;
        }
    }
}
