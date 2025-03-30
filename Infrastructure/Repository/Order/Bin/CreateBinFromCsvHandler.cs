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
    public class CreateBinFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateBinFromCsvCommand, List<CreateBinResponseDTO>>
    {
        public async Task<List<CreateBinResponseDTO>> Handle(CreateBinFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateBinRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var binsToCreate = new List<Bin>();
                await foreach (var record in records)
                {
                    var bin = new Bin()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Name = record.Name,
                        CustomerOrders = await wmsDbContext.CustomerOrders.Where(customerOrder=>record.CustomerOrderIds.Contains(customerOrder.Id)).ToListAsync(cancellationToken),
                    };
                    binsToCreate.Add(bin);
                }

                await wmsDbContext.AddRangeAsync(binsToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var binsCreated = new List<CreateBinResponseDTO>();
                foreach (var binToCreate in binsToCreate)
                {
                    var createBinResponseDTO = new CreateBinResponseDTO()
                    {
                        Id = binToCreate.Id,
                        Name = binToCreate.Name,
                        CreatedBy = binToCreate.CreatedBy,
                        CustomerOrderIds = binToCreate.CustomerOrders.Select(customerOrder=>customerOrder.Id).ToList(),
                    };
                    binsCreated.Add(createBinResponseDTO);
                }

                return binsCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateBinResponseDTO>();
            }
        }
    }
}
