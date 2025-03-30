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
    public class CreateCustomerFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateCustomerFromCsvCommand, List<CreateCustomerResponseDTO>>
    {
        public async Task<List<CreateCustomerResponseDTO>> Handle(CreateCustomerFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateCustomerRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var customersToCreate = new List<Customer>();
                await foreach (var record in records)
                {
                    var customer = new Customer()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        CustomerOrders = await wmsDbContext.CustomerOrders.Where(customerOrder=>record.CustomerOrderIds.Contains(customerOrder.Id)).ToListAsync(cancellationToken),
                        Address = record.Address,
                        Email = record.Email,
                        FirstName = record.FirstName,
                        LastName = record.LastName,
                    };
                    customersToCreate.Add(customer);
                }

                await wmsDbContext.AddRangeAsync(customersToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var customersCreated = new List<CreateCustomerResponseDTO>();
                foreach (var customerToCreate in customersToCreate)
                {
                    var createCustomerResponseDTO = new CreateCustomerResponseDTO()
                    {
                        Id = customerToCreate.Id,
                        CreatedBy = customerToCreate.CreatedBy,
                        Address = customerToCreate.Address,
                        CustomerOrderIds = customerToCreate.CustomerOrders.Select(customerOrder => customerOrder.Id).ToList(),
                        Email = customerToCreate.Email,
                        FirstName = customerToCreate.FirstName,
                        LastName = customerToCreate.LastName,
                        
                    };
                    customersCreated.Add(createCustomerResponseDTO);
                }

                return customersCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateCustomerResponseDTO>();
            }
        }
    }
}
