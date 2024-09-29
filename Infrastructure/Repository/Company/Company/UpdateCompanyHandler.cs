using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateCompanyHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateCompanyCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var companyFound = await wmsDbContext.Companies.FirstOrDefaultAsync(
                    company => company.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (companyFound == null)
                {
                    return GeneralDbResponses.ItemNotFound(request.Model.CompanyName);
                }

                wmsDbContext.Entry(companyFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<Company>();
                wmsDbContext.Companies.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated(request.Model.CompanyName);
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
