using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CreateCompanyHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateCompanyCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundCompany = await wmsDbContext.Companies.FirstOrDefaultAsync(
                    company => string.Equals(company.Name, request.Model.CompanyName, StringComparison.CurrentCultureIgnoreCase),
                    cancellationToken);
                if (foundCompany != null)
                {
                    return GeneralDbResponses.ItemAlreadyExist(request.Model.CompanyName);
                }

                var data = request.Model.Adapt<Company>();
                wmsDbContext.Companies.Add(data);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemCreated(request.Model.CompanyName);
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
