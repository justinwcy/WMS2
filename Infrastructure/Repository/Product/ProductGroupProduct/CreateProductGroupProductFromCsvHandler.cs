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
    public class CreateProductGroupProductFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateProductGroupProductFromCsvCommand, List<CreateProductGroupProductResponseDTO>>
    {
        public async Task<List<CreateProductGroupProductResponseDTO>> Handle(CreateProductGroupProductFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateProductGroupProductRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var productGroupProductsToCreate = new List<ProductGroupProduct>();
                await foreach (var record in records)
                {
                    var productGroupProduct = new ProductGroupProduct()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        ProductId = record.ProductId,
                        Product = await wmsDbContext.Products.FirstAsync(product=>product.Id==record.ProductId, cancellationToken),
                        ProductGroupId = record.ProductGroupId,
                        ProductGroup = await wmsDbContext.ProductGroups.FirstAsync(productGroup => productGroup.Id == record.ProductGroupId, cancellationToken),
                    };
                    productGroupProductsToCreate.Add(productGroupProduct);
                }

                await wmsDbContext.AddRangeAsync(productGroupProductsToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var productGroupProductsCreated = new List<CreateProductGroupProductResponseDTO>();
                foreach (var productGroupProductToCreate in productGroupProductsToCreate)
                {
                    var createProductGroupProductResponseDTO = new CreateProductGroupProductResponseDTO()
                    {
                        Id = productGroupProductToCreate.Id,
                        CreatedBy = productGroupProductToCreate.CreatedBy,
                        ProductGroupId = productGroupProductToCreate.ProductGroupId,
                        ProductId = productGroupProductToCreate.ProductId,
                        
                    };
                    productGroupProductsCreated.Add(createProductGroupProductResponseDTO);
                }

                return productGroupProductsCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateProductGroupProductResponseDTO>();
            }
        }
    }
}
