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
    public class CreateProductFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateProductFromCsvCommand, List<CreateProductResponseDTO>>
    {
        public async Task<List<CreateProductResponseDTO>> Handle(CreateProductFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateProductRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var productsToCreate = new List<Product>();
                await foreach (var record in records)
                {
                    var product = new Product()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Name = record.Name,
                        Description = record.Description,
                        Width = record.Width,
                        Height = record.Height,
                        Length = record.Length,
                        Sku = record.Sku,
                        Price = record.Price,
                        Tag = record.Tag,
                        Weight = record.Weight,
                        Shops = await wmsDbContext.Shops.Where(shop=>record.ShopIds.Contains(shop.Id)).ToListAsync(cancellationToken),
                        CustomerOrderDetails = await wmsDbContext.CustomerOrderDetails.Where(customerOrderDetail=>record.CustomerOrderDetailIds.Contains(customerOrderDetail.Id)).ToListAsync(cancellationToken),
                        IncomingOrders = await wmsDbContext.IncomingOrders.Where(incomingOrder => record.IncomingOrderIds.Contains(incomingOrder.Id)).ToListAsync(cancellationToken),
                        ProductGroups = await wmsDbContext.ProductGroups.Where(productGroup => record.ProductGroupIds.Contains(productGroup.Id)).ToListAsync(cancellationToken),
                        Racks = await wmsDbContext.Racks.Where(rack => record.RackIds.Contains(rack.Id)).ToListAsync(cancellationToken),
                        RefundOrders = await wmsDbContext.RefundOrders.Where(refundOrder => record.RefundOrderIds.Contains(refundOrder.Id)).ToListAsync(cancellationToken),

                    };
                    productsToCreate.Add(product);
                }

                await wmsDbContext.AddRangeAsync(productsToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var productsCreated = new List<CreateProductResponseDTO>();
                foreach (var productToCreate in productsToCreate)
                {
                    var createProductResponseDTO = new CreateProductResponseDTO()
                    {
                        Id = productToCreate.Id,
                        CreatedBy = productToCreate.CreatedBy,
                        Name = productToCreate.Name,
                        Description = productToCreate.Description,
                        Width = productToCreate.Width.Value,
                        Height = productToCreate.Height.Value,
                        Length = productToCreate.Length.Value,
                        Sku = productToCreate.Sku,
                        Price = productToCreate.Price,
                        Tag = productToCreate.Tag,
                        Weight = productToCreate.Weight,
                        RackIds = productToCreate.Racks.Select(rack=>rack.Id).ToList(),
                        IncomingOrderIds = productToCreate.IncomingOrders.Select(incomingOrder=> incomingOrder.Id).ToList(),
                        CustomerOrderDetailIds = productToCreate.CustomerOrderDetails.Select(customerOrderDetails => customerOrderDetails.Id).ToList(),
                        ProductGroupIds = productToCreate.ProductGroups.Select(productGroup=> productGroup.Id).ToList(),
                        RefundOrderIds = productToCreate.RefundOrders.Select(refundOrder=> refundOrder.Id).ToList(),
                        ShopIds = productToCreate.Shops.Select(shop=>shop.Id).ToList(),

                    };
                    productsCreated.Add(createProductResponseDTO);
                }

                return productsCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateProductResponseDTO>();
            }
        }
    }
}
