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
    public class CreateShopFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateShopFromCsvCommand, List<CreateShopResponseDTO>>
    {
        public async Task<List<CreateShopResponseDTO>> Handle(CreateShopFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateShopRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var shopsToCreate = new List<Shop>();
                await foreach (var record in records)
                {
                    var shop = new Shop()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Name = record.Name,
                        Platform = record.Platform,
                        Address = record.Address,
                        Website = record.Website,
                        Products = await wmsDbContext.Products.Where(product=>record.ProductIds.Contains(product.Id)).ToListAsync(cancellationToken),
                        
                    };
                    shopsToCreate.Add(shop);
                }

                await wmsDbContext.AddRangeAsync(shopsToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var shopsCreated = new List<CreateShopResponseDTO>();
                foreach (var shopToCreate in shopsToCreate)
                {
                    var createShopResponseDTO = new CreateShopResponseDTO()
                    {
                        Id = shopToCreate.Id,
                        CreatedBy = shopToCreate.CreatedBy,
                        Name = shopToCreate.Name,
                        Platform = shopToCreate.Platform,
                        Address = shopToCreate.Address,
                        Website = shopToCreate.Website,
                        ProductIds = shopToCreate.Products.Select(product=>product.Id).ToList(),
                        
                    };
                    shopsCreated.Add(createShopResponseDTO);
                }

                return shopsCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateShopResponseDTO>();
            }
        }
    }
}
