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
    public class CreateWarehouseFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateWarehouseFromCsvCommand, List<CreateWarehouseResponseDTO>>
    {
        public async Task<List<CreateWarehouseResponseDTO>> Handle(CreateWarehouseFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateWarehouseRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var warehousesToCreate = new List<Warehouse>();
                await foreach (var record in records)
                {
                    var warehouse = new Warehouse()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Name = record.Name,
                        Address = record.Address,
                        CompanyId = record.CompanyId,
                        Company = await wmsDbContext.Companies.FirstAsync(company=> company.Id == record.CompanyId, cancellationToken),
                        Zones = await wmsDbContext.Zones.Where(zone => record.ZoneIds.Contains(zone.Id)).ToListAsync(cancellationToken),
                    };
                    warehousesToCreate.Add(warehouse);
                }

                await wmsDbContext.AddRangeAsync(warehousesToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var warehousesCreated = new List<CreateWarehouseResponseDTO>();
                foreach (var warehouseToCreate in warehousesToCreate)
                {
                    var createWarehouseResponseDTO = new CreateWarehouseResponseDTO()
                    {
                        Id = warehouseToCreate.Id,
                        Name = warehouseToCreate.Name,
                        CreatedBy = warehouseToCreate.CreatedBy,
                        Address = warehouseToCreate.Address,
                        CompanyId = warehouseToCreate.CompanyId,
                        ZoneIds = warehouseToCreate.Zones.Select(zone=>zone.Id).ToList(),
                    };
                    warehousesCreated.Add(createWarehouseResponseDTO);
                }

                return warehousesCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateWarehouseResponseDTO>();
            }
        }
    }
}
