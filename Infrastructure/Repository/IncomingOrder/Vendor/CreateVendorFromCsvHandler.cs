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
    public class CreateVendorFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateVendorFromCsvCommand, List<CreateVendorResponseDTO>>
    {
        public async Task<List<CreateVendorResponseDTO>> Handle(CreateVendorFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateVendorRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var vendorsToCreate = new List<Vendor>();
                await foreach (var record in records)
                {
                    var vendor = new Vendor()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Address = record.Address,
                        Email = record.Email,
                        FirstName = record.FirstName,
                        LastName = record.LastName,
                        IncomingOrders = await wmsDbContext.IncomingOrders.Where(incomingOrder=>record.IncomingOrderIds.Contains(incomingOrder.Id)).ToListAsync(cancellationToken),
                        PhoneNumber = record.PhoneNumber,
                    };
                    vendorsToCreate.Add(vendor);
                }

                await wmsDbContext.AddRangeAsync(vendorsToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var vendorsCreated = new List<CreateVendorResponseDTO>();
                foreach (var vendorToCreate in vendorsToCreate)
                {
                    var createVendorResponseDTO = new CreateVendorResponseDTO()
                    {
                        Id = vendorToCreate.Id,
                        CreatedBy = vendorToCreate.CreatedBy,
                        Address = vendorToCreate.Address,
                        Email = vendorToCreate.Email,
                        FirstName = vendorToCreate.FirstName,
                        LastName = vendorToCreate.LastName,
                        IncomingOrderIds = vendorToCreate.IncomingOrders.Select(incomingOrder => incomingOrder.Id).ToList(),
                        PhoneNumber = vendorToCreate.PhoneNumber,
                    };
                    vendorsCreated.Add(createVendorResponseDTO);
                }

                return vendorsCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateVendorResponseDTO>();
            }
        }
    }
}
