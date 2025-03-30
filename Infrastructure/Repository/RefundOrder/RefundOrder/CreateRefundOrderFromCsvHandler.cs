using System.Linq;

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
    public class CreateRefundOrderFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateRefundOrderFromCsvCommand, List<CreateRefundOrderResponseDTO>>
    {
        public async Task<List<CreateRefundOrderResponseDTO>> Handle(CreateRefundOrderFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateRefundOrderRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var refundOrdersToCreate = new List<RefundOrder>();
                await foreach (var record in records)
                {
                    var refundOrder = new RefundOrder()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Status = record.Status,
                        RefundReason = record.RefundReason,
                        RefundDate = record.RefundDate,
                        RefundOrderProducts = await wmsDbContext.RefundOrderProducts.Where(refundOrderProduct=>record.RefundOrderProductIds.Contains(refundOrderProduct.Id)).ToListAsync(cancellationToken),
                        
                    };
                    refundOrdersToCreate.Add(refundOrder);
                }

                await wmsDbContext.AddRangeAsync(refundOrdersToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var refundOrdersCreated = new List<CreateRefundOrderResponseDTO>();
                foreach (var refundOrderToCreate in refundOrdersToCreate)
                {
                    var createRefundOrderResponseDTO = new CreateRefundOrderResponseDTO()
                    {
                        Id = refundOrderToCreate.Id,
                        CreatedBy = refundOrderToCreate.CreatedBy,
                        Status = refundOrderToCreate.Status,
                        RefundDate = refundOrderToCreate.RefundDate,
                        RefundReason = refundOrderToCreate.RefundReason,
                        RefundOrderProductIds = refundOrderToCreate.RefundOrderProducts.Select(refundOrderProduct=> refundOrderProduct.Id).ToList(),
                    };
                    refundOrdersCreated.Add(createRefundOrderResponseDTO);
                }

                return refundOrdersCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateRefundOrderResponseDTO>();
            }
        }
    }
}
