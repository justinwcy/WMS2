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
    public class CreateInventoryFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateInventoryFromCsvCommand, List<CreateInventoryResponseDTO>>
    {
        public async Task<List<CreateInventoryResponseDTO>> Handle(CreateInventoryFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateInventoryRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var inventoriesToCreate = new List<Inventory>();
                await foreach (var record in records)
                {
                    var inventory = new Inventory()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Product = await wmsDbContext.Products.FirstAsync(product => product.Id == record.ProductId, cancellationToken),
                        ProductId = record.ProductId,
                        Quantity = record.Quantity,
                        Rack = await wmsDbContext.Racks.FirstAsync(rack => rack.Id == record.RackId, cancellationToken),
                        RackId = record.RackId,
                    };
                    inventoriesToCreate.Add(inventory);
                }

                await wmsDbContext.AddRangeAsync(inventoriesToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var inventoriesCreated = new List<CreateInventoryResponseDTO>();
                foreach (var inventoryToCreate in inventoriesToCreate)
                {
                    var createInventoryResponseDTO = new CreateInventoryResponseDTO()
                    {
                        Id = inventoryToCreate.Id,
                        CreatedBy = inventoryToCreate.CreatedBy,
                        ProductId = inventoryToCreate.ProductId,
                        Quantity = inventoryToCreate.Quantity,
                        RackId = inventoryToCreate.RackId,
                    };
                    inventoriesCreated.Add(createInventoryResponseDTO);
                }

                return inventoriesCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateInventoryResponseDTO>();
            }
        }
    }
}
