using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteCompanyHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteCompanyCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundCompany = await wmsDbContext.Companies.FirstOrDefaultAsync(
                    company => company.Id == request.Id,
                    cancellationToken);
                if (foundCompany == null)
                {
                    return GeneralDbResponses.ItemNotFound("Company");
                }

                wmsDbContext.Companies.Remove(foundCompany);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted(foundCompany.Name);
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
