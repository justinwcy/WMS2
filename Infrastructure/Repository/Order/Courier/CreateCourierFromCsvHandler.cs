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
    public class CreateCourierFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateCourierFromCsvCommand, List<CreateCourierResponseDTO>>
    {
        public async Task<List<CreateCourierResponseDTO>> Handle(CreateCourierFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateCourierRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var couriersToCreate = new List<Courier>();
                await foreach (var record in records)
                {
                    var courier = new Courier()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Name = record.Name,
                        Price = record.Price,
                        Remark = record.Remark,
                        CustomerOrders = await wmsDbContext.CustomerOrders.Where(customerOrder=>record.CustomerOrderIds.Contains(customerOrder.Id)).ToListAsync(cancellationToken),
                    };
                    couriersToCreate.Add(courier);
                }

                await wmsDbContext.AddRangeAsync(couriersToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var couriersCreated = new List<CreateCourierResponseDTO>();
                foreach (var courierToCreate in couriersToCreate)
                {
                    var createCourierResponseDTO = new CreateCourierResponseDTO()
                    {
                        Id = courierToCreate.Id,
                        Name = courierToCreate.Name,
                        CreatedBy = courierToCreate.CreatedBy,
                        Price = courierToCreate.Price,
                        Remark = courierToCreate.Remark,
                        CustomerOrderIds = courierToCreate.CustomerOrders.Select(customerOrder => customerOrder.Id).ToList(),
                    };
                    couriersCreated.Add(createCourierResponseDTO);
                }

                return couriersCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateCourierResponseDTO>();
            }
        }
    }
}
