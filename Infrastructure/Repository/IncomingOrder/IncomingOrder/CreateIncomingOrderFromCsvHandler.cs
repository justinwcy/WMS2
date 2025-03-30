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
    public class CreateIncomingOrderFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateIncomingOrderFromCsvCommand, List<CreateIncomingOrderResponseDTO>>
    {
        public async Task<List<CreateIncomingOrderResponseDTO>> Handle(CreateIncomingOrderFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateIncomingOrderRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var incomingOrdersToCreate = new List<IncomingOrder>();
                await foreach (var record in records)
                {
                    var incomingOrder = new IncomingOrder()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Status = record.Status,
                        IncomingDate = record.IncomingDate,
                        IncomingOrderProducts = await wmsDbContext.IncomingOrderProducts.Where(incomingOrderProduct => record.IncomingOrderProductIds.Contains(incomingOrderProduct.Id)).ToListAsync(cancellationToken),
                        PONumber = record.PONumber,
                        ReceivingDate = record.ReceivingDate,
                        Vendor = await wmsDbContext.Vendors.FirstAsync(vendor => record.VendorId == vendor.Id, cancellationToken),
                        VendorId = record.VendorId,
                    };
                    incomingOrdersToCreate.Add(incomingOrder);
                }

                await wmsDbContext.AddRangeAsync(incomingOrdersToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var incomingOrdersCreated = new List<CreateIncomingOrderResponseDTO>();
                foreach (var incomingOrderToCreate in incomingOrdersToCreate)
                {
                    var createIncomingOrderResponseDTO = new CreateIncomingOrderResponseDTO()
                    {
                        Id = incomingOrderToCreate.Id,
                        CreatedBy = incomingOrderToCreate.CreatedBy,
                        Status = incomingOrderToCreate.Status,
                        IncomingDate = incomingOrderToCreate.IncomingDate,
                        PONumber = incomingOrderToCreate.PONumber,
                        ReceivingDate = incomingOrderToCreate.ReceivingDate,
                        VendorId = incomingOrderToCreate.VendorId,
                        IncomingOrderProductIds = incomingOrderToCreate.IncomingOrderProducts.Select(incomingOrderProduct=>incomingOrderProduct.Id).ToList(),
                    };
                    incomingOrdersCreated.Add(createIncomingOrderResponseDTO);
                }

                return incomingOrdersCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateIncomingOrderResponseDTO>();
            }
        }
    }
}
