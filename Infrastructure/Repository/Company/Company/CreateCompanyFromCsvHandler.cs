using Application.DTO.Request;
using Application.DTO.Response;
using Application.Service.Commands;

using CsvHelper;

using Domain.Entities;

using Infrastructure.Data;
using Infrastructure.Extensions;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CreateCompanyFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateCompanyFromCsvCommand, List<CreateCompanyResponseDTO>>
    {
        public async Task<List<CreateCompanyResponseDTO>> Handle(CreateCompanyFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateCompanyRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var companiesToCreate = new List<Company>();
                await foreach (var record in records)
                {
                    var company = new Company()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Name = record.Name,
                        Staffs = await wmsDbContext.Staffs.Where(staff => record.StaffIds.Contains(staff.Id)).ToListAsync(cancellationToken),
                        Warehouses = await wmsDbContext.Warehouses.Where(warehouse => record.WarehouseIds.Contains(warehouse.Id)).ToListAsync(cancellationToken),
                    };
                    companiesToCreate.Add(company);
                }

                await wmsDbContext.AddRangeAsync(companiesToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var companiesCreated = new List<CreateCompanyResponseDTO>();
                foreach (var companyToCreate in companiesToCreate)
                {
                    var createCompanyResponseDTO = new CreateCompanyResponseDTO()
                    {
                        Id = companyToCreate.Id,
                        Name = companyToCreate.Name,
                        CreatedBy = companyToCreate.CreatedBy,
                        StaffIds = companyToCreate.Staffs.Select(staff => staff.Id).ToList(),
                        WarehouseIds = companyToCreate.Warehouses.Select(warehouse => warehouse.Id).ToList(),
                    };
                    companiesCreated.Add(createCompanyResponseDTO);
                }

                return companiesCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateCompanyResponseDTO>();
            }
        }
    }
}
