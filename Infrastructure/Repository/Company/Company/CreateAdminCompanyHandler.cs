using Application.Constants;
using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CreateAdminCompanyHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateAdminCompanyCommand, CreateCompanyResponseDTO>
    {
        public async Task<CreateCompanyResponseDTO> Handle(CreateAdminCompanyCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var foundCompany = await wmsDbContext.Companies.FirstOrDefaultAsync(
                company => company.Id == DebugConstants.CompanyId,
                cancellationToken);
            if (foundCompany != null)
            {
                throw new Exception(GeneralResponses.ItemAlreadyExist("Admin Company"));
            }

            var adminCompany = new Company()
            {
                CreatedBy = DebugConstants.AdminId,
                Id = DebugConstants.CompanyId,
                Name = "Admin Company",
                Staffs = [],
                Warehouses = [],
            };
            var companyCreated = wmsDbContext.Companies.Add(adminCompany);
            await wmsDbContext.SaveChangesAsync(cancellationToken);

            return new CreateCompanyResponseDTO(){ Id = companyCreated.Entity.Id };

        }
    }
}
