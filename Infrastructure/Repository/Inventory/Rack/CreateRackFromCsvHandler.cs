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
    public class CreateRackFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateRackFromCsvCommand, List<CreateRackResponseDTO>>
    {
        public async Task<List<CreateRackResponseDTO>> Handle(CreateRackFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateRackRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var racksToCreate = new List<Rack>();
                await foreach (var record in records)
                {
                    var rack = new Rack()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Name = record.Name,
                        Depth = record.Depth,
                        Height = record.Height,
                        MaxWeight = record.MaxWeight,
                        Width = record.Width,
                        Products = await wmsDbContext.Products.Where(product=> record.ProductIds.Contains(product.Id)).ToListAsync(cancellationToken),
                        Zone = await wmsDbContext.Zones.FirstAsync(zone=>zone.Id == record.ZoneId, cancellationToken),
                        ZoneId = record.ZoneId,
                    };
                    racksToCreate.Add(rack);
                }

                await wmsDbContext.AddRangeAsync(racksToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var racksCreated = new List<CreateRackResponseDTO>();
                foreach (var rackToCreate in racksToCreate)
                {
                    var createRackResponseDTO = new CreateRackResponseDTO()
                    {
                        Id = rackToCreate.Id,
                        Name = rackToCreate.Name,
                        CreatedBy = rackToCreate.CreatedBy,
                        Depth = rackToCreate.Depth,
                        Height = rackToCreate.Height,
                        MaxWeight = rackToCreate.MaxWeight,
                        Width = rackToCreate.Width,
                        ZoneId = rackToCreate.ZoneId,
                        ProductIds = rackToCreate.Products.Select(product=>product.Id).ToList(),
                    };
                    racksCreated.Add(createRackResponseDTO);
                }

                return racksCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateRackResponseDTO>();
            }
        }
    }
}
