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
    public class CreateZoneStaffFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateZoneStaffFromCsvCommand, List<CreateZoneStaffResponseDTO>>
    {
        public async Task<List<CreateZoneStaffResponseDTO>> Handle(CreateZoneStaffFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateZoneStaffRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var zoneStaffsToCreate = new List<ZoneStaff>();
                await foreach (var record in records)
                {
                    var zoneStaff = new ZoneStaff()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Staff = await wmsDbContext.Staffs.FirstAsync(staff=>staff.Id==record.StaffId, cancellationToken),
                        StaffId = record.StaffId,
                        Zone = await wmsDbContext.Zones.FirstAsync(zone => zone.Id==record.ZoneId, cancellationToken),
                        ZoneId = record.ZoneId,
                    };
                    zoneStaffsToCreate.Add(zoneStaff);
                }

                await wmsDbContext.AddRangeAsync(zoneStaffsToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var zoneStaffsCreated = new List<CreateZoneStaffResponseDTO>();
                foreach (var zoneStaffToCreate in zoneStaffsToCreate)
                {
                    var createZoneStaffResponseDTO = new CreateZoneStaffResponseDTO()
                    {
                        Id = zoneStaffToCreate.Id,
                        CreatedBy = zoneStaffToCreate.CreatedBy,
                        StaffId = zoneStaffToCreate.StaffId,
                        ZoneId = zoneStaffToCreate.ZoneId,
                        
                    };
                    zoneStaffsCreated.Add(createZoneStaffResponseDTO);
                }

                return zoneStaffsCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateZoneStaffResponseDTO>();
            }
        }
    }
}
