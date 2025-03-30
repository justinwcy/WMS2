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
    public class CreateRefundOrderProductFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateRefundOrderProductFromCsvCommand, List<CreateRefundOrderProductResponseDTO>>
    {
        public async Task<List<CreateRefundOrderProductResponseDTO>> Handle(CreateRefundOrderProductFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateRefundOrderProductRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var refundOrderProductsToCreate = new List<RefundOrderProduct>();
                await foreach (var record in records)
                {
                    var refundOrderProduct = new RefundOrderProduct()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Status = record.Status,
                        Product = await wmsDbContext.Products.FirstAsync(product=>product.Id == record.ProductId, cancellationToken),
                        ProductId = record.ProductId,
                        Quantity = record.Quantity,
                        RefundOrder = await wmsDbContext.RefundOrders.FirstAsync(refundOrder => refundOrder.Id == record.RefundOrderId, cancellationToken),
                        RefundOrderId = record.RefundOrderId,
                    };
                    refundOrderProductsToCreate.Add(refundOrderProduct);
                }

                await wmsDbContext.AddRangeAsync(refundOrderProductsToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var refundOrderProductsCreated = new List<CreateRefundOrderProductResponseDTO>();
                foreach (var refundOrderProductToCreate in refundOrderProductsToCreate)
                {
                    var createRefundOrderProductResponseDTO = new CreateRefundOrderProductResponseDTO()
                    {
                        Id = refundOrderProductToCreate.Id,
                        CreatedBy = refundOrderProductToCreate.CreatedBy,
                        ProductId = refundOrderProductToCreate.ProductId,
                        Status = refundOrderProductToCreate.Status,
                        Quantity = refundOrderProductToCreate.Quantity,
                        RefundOrderId = refundOrderProductToCreate.RefundOrderId,
                        
                    };
                    refundOrderProductsCreated.Add(createRefundOrderProductResponseDTO);
                }

                return refundOrderProductsCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateRefundOrderProductResponseDTO>();
            }
        }
    }
}
