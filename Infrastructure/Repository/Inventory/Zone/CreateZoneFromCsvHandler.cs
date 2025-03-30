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
    public class CreateZoneFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateZoneFromCsvCommand, List<CreateZoneResponseDTO>>
    {
        public async Task<List<CreateZoneResponseDTO>> Handle(CreateZoneFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateZoneRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var zonesToCreate = new List<Zone>();
                await foreach (var record in records)
                {
                    var zone = new Zone()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Name = record.Name,
                        Staffs = await wmsDbContext.Staffs.Where(staff => record.StaffIds.Contains(staff.Id)).ToListAsync(cancellationToken),
                        Racks = await wmsDbContext.Racks.Where(rack => record.RackIds.Contains(rack.Id)).ToListAsync(cancellationToken),
                        Warehouse = await wmsDbContext.Warehouses.FirstAsync(warehouse=>warehouse.Id==record.WarehouseId, cancellationToken),
                        WarehouseId = record.WarehouseId,
                    };
                    zonesToCreate.Add(zone);
                }

                await wmsDbContext.AddRangeAsync(zonesToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var zonesCreated = new List<CreateZoneResponseDTO>();
                foreach (var zoneToCreate in zonesToCreate)
                {
                    var createZoneResponseDTO = new CreateZoneResponseDTO()
                    {
                        Id = zoneToCreate.Id,
                        Name = zoneToCreate.Name,
                        CreatedBy = zoneToCreate.CreatedBy,
                        StaffIds = zoneToCreate.Staffs.Select(staff => staff.Id).ToList(),
                        WarehouseId = zoneToCreate.WarehouseId,
                        RackIds = zoneToCreate.Racks.Select(rack=>rack.Id).ToList(),
                        
                    };
                    zonesCreated.Add(createZoneResponseDTO);
                }

                return zonesCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateZoneResponseDTO>();
            }
        }
    }
}
