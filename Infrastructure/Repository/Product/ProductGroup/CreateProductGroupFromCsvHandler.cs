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
    public class CreateProductGroupFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateProductGroupFromCsvCommand, List<CreateProductGroupResponseDTO>>
    {
        public async Task<List<CreateProductGroupResponseDTO>> Handle(CreateProductGroupFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateProductGroupRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var productGroupsToCreate = new List<ProductGroup>();
                await foreach (var record in records)
                {
                    
                    var productGroup = new ProductGroup()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Name = record.Name,
                        Products = await wmsDbContext.Products.Where(product=>record.ProductIds.Contains(product.Id)).ToListAsync(cancellationToken),
                        Description = record.Description,
                        Photos = await wmsDbContext.FileStorages.Where(fileStorage=>record.PhotoIds.Contains(fileStorage.Id)).ToListAsync(cancellationToken),
                    };
                    productGroupsToCreate.Add(productGroup);
                }

                await wmsDbContext.AddRangeAsync(productGroupsToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var productGroupsCreated = new List<CreateProductGroupResponseDTO>();
                foreach (var productGroupToCreate in productGroupsToCreate)
                {
                    var createProductGroupResponseDTO = new CreateProductGroupResponseDTO()
                    {
                        Id = productGroupToCreate.Id,
                        Name = productGroupToCreate.Name,
                        CreatedBy = productGroupToCreate.CreatedBy,
                        ProductIds = productGroupToCreate.Products.Select(product=>product.Id).ToList(),
                        Description = productGroupToCreate.Description,
                        PhotoIds = productGroupToCreate.Photos.Select(photo => photo.Id).ToList()
                    };
                    productGroupsCreated.Add(createProductGroupResponseDTO);
                }

                return productGroupsCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateProductGroupResponseDTO>();
            }
        }
    }
}
