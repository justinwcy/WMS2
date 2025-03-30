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
    public class CreateIncomingOrderProductFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateIncomingOrderProductFromCsvCommand, List<CreateIncomingOrderProductResponseDTO>>
    {
        public async Task<List<CreateIncomingOrderProductResponseDTO>> Handle(CreateIncomingOrderProductFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateIncomingOrderProductRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var incomingOrderProductsToCreate = new List<IncomingOrderProduct>();
                await foreach (var record in records)
                {
                    var incomingOrderProduct = new IncomingOrderProduct()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Status = record.Status,
                        IncomingOrder = await wmsDbContext.IncomingOrders.FirstAsync(incomingOrder=>incomingOrder.Id==record.IncomingOrderId, cancellationToken),
                        IncomingOrderId = record.IncomingOrderId,
                        Product = await wmsDbContext.Products.FirstAsync(product => product.Id == record.ProductId, cancellationToken),
                        ProductId = record.ProductId,
                        Quantity = record.Quantity,
                    };
                    incomingOrderProductsToCreate.Add(incomingOrderProduct);
                }

                await wmsDbContext.AddRangeAsync(incomingOrderProductsToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var incomingOrderProductsCreated = new List<CreateIncomingOrderProductResponseDTO>();
                foreach (var incomingOrderProductToCreate in incomingOrderProductsToCreate)
                {
                    var createIncomingOrderProductResponseDTO = new CreateIncomingOrderProductResponseDTO()
                    {
                        Id = incomingOrderProductToCreate.Id,
                        CreatedBy = incomingOrderProductToCreate.CreatedBy,
                        Status = incomingOrderProductToCreate.Status,
                        IncomingOrderId = incomingOrderProductToCreate.IncomingOrderId,
                        ProductId = incomingOrderProductToCreate.ProductId,
                        Quantity = incomingOrderProductToCreate.Quantity,
                    };
                    incomingOrderProductsCreated.Add(createIncomingOrderProductResponseDTO);
                }

                return incomingOrderProductsCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateIncomingOrderProductResponseDTO>();
            }
        }
    }
}
