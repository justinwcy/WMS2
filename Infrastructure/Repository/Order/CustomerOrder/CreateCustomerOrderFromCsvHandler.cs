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
    public class CreateCustomerOrderFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateCustomerOrderFromCsvCommand, List<CreateCustomerOrderResponseDTO>>
    {
        public async Task<List<CreateCustomerOrderResponseDTO>> Handle(CreateCustomerOrderFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateCustomerOrderRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var customerOrdersToCreate = new List<CustomerOrder>();
                await foreach (var record in records)
                {
                    var customerOrder = new CustomerOrder()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Bin = await wmsDbContext.Bins.FirstAsync(bin=>bin.Id==record.BinId, cancellationToken),
                        BinId = record.BinId,
                        Courier = await wmsDbContext.Couriers.FirstAsync(courier => courier.Id == record.CourierId, cancellationToken),
                        CourierId = record.CourierId,
                        Customer = await wmsDbContext.Customers.FirstAsync(customer => customer.Id == record.CustomerId, cancellationToken),
                        CustomerId = record.CustomerId,
                        ExpectedArrivalDate = record.ExpectedArrivalDate,
                        OrderAddress = record.OrderAddress,
                        OrderCreationDate = record.OrderCreationDate,
                        CustomerOrderDetails = await wmsDbContext.CustomerOrderDetails.Where(customerOrderDetail=>record.CustomerOrderDetailIds.Contains(customerOrderDetail.Id)).ToListAsync(cancellationToken),
                        
                    };
                    customerOrdersToCreate.Add(customerOrder);
                }

                await wmsDbContext.AddRangeAsync(customerOrdersToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var customerOrdersCreated = new List<CreateCustomerOrderResponseDTO>();
                foreach (var customerOrderToCreate in customerOrdersToCreate)
                {
                    var createCustomerOrderResponseDTO = new CreateCustomerOrderResponseDTO()
                    {
                        Id = customerOrderToCreate.Id,
                        CreatedBy = customerOrderToCreate.CreatedBy,
                        CustomerId = customerOrderToCreate.CustomerId,
                        BinId = customerOrderToCreate.BinId,
                        OrderAddress = customerOrderToCreate.OrderAddress,
                        OrderCreationDate = customerOrderToCreate.OrderCreationDate,
                        CourierId = customerOrderToCreate.CourierId,
                        CustomerOrderDetailIds = customerOrderToCreate.CustomerOrderDetails.Select(customerOrderDetail=>customerOrderDetail.Id).ToList(),
                        ExpectedArrivalDate = customerOrderToCreate.ExpectedArrivalDate,
                        
                    };
                    customerOrdersCreated.Add(createCustomerOrderResponseDTO);
                }

                return customerOrdersCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateCustomerOrderResponseDTO>();
            }
        }
    }
}
