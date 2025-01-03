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
        IRequestHandler<CreateCompanyCommand, CreateCompanyResponseDTO>
    {
        public async Task<CreateCompanyResponseDTO> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var foundCompany = await wmsDbContext.Companies.FirstOrDefaultAsync(
                company => string.Equals(company.Name, request.Model.CompanyName, StringComparison.CurrentCultureIgnoreCase),
                cancellationToken);
            if (foundCompany != null)
            {
                throw new Exception(GeneralResponses.ItemAlreadyExist(request.Model.CompanyName));
            }

            var data = request.Model.Adapt<Company>();
            var companyCreated = wmsDbContext.Companies.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);

            return new CreateCompanyResponseDTO(){ Id = companyCreated.Entity.Id };

        }
    }
}
