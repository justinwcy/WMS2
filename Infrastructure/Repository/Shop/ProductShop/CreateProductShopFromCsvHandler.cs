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
    public class CreateProductShopFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateProductShopFromCsvCommand, List<CreateProductShopResponseDTO>>
    {
        public async Task<List<CreateProductShopResponseDTO>> Handle(CreateProductShopFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateProductShopRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var productShopsToCreate = new List<ProductShop>();
                await foreach (var record in records)
                {
                    var productShop = new ProductShop()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Product = await wmsDbContext.Products.FirstAsync(product=>product.Id == record.ProductId, cancellationToken),
                        ProductId = record.ProductId,
                        Shop = await wmsDbContext.Shops.FirstAsync(shop=>shop.Id==record.ShopId, cancellationToken),
                        ShopId = record.ShopId,
                        
                    };
                    productShopsToCreate.Add(productShop);
                }

                await wmsDbContext.AddRangeAsync(productShopsToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var productShopsCreated = new List<CreateProductShopResponseDTO>();
                foreach (var productShopToCreate in productShopsToCreate)
                {
                    var createProductShopResponseDTO = new CreateProductShopResponseDTO()
                    {
                        Id = productShopToCreate.Id,
                        CreatedBy = productShopToCreate.CreatedBy,
                        ShopId = productShopToCreate.ShopId,
                        ProductId = productShopToCreate.ProductId,
                        
                    };
                    productShopsCreated.Add(createProductShopResponseDTO);
                }

                return productShopsCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateProductShopResponseDTO>();
            }
        }
    }
}
