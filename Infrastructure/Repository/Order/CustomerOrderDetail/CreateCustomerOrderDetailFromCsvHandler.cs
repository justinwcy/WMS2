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
    public class CreateCustomerOrderDetailFromCsvHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateCustomerOrderDetailFromCsvCommand, List<CreateCustomerOrderDetailResponseDTO>>
    {
        public async Task<List<CreateCustomerOrderDetailResponseDTO>> Handle(CreateCustomerOrderDetailFromCsvCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var reader = new StreamReader(request.Model.CsvFileStream);
                using var csv = new CsvReader(reader, CsvUtilities.GetCsvConfiguration());
                var records = csv.GetRecordsAsync<CreateCustomerOrderDetailRequestDTO>(cancellationToken);

                await using var wmsDbContext = contextFactory.CreateDbContext();
                var customerOrderDetailsToCreate = new List<CustomerOrderDetail>();
                await foreach (var record in records)
                {
                    var customerOrderDetail = new CustomerOrderDetail()
                    {
                        CreatedBy = request.Model.CreatedBy,
                        Status = record.Status,
                        ProductId = record.ProductId,
                        Product = await wmsDbContext.Products.FirstAsync(product=>product.Id == record.ProductId, cancellationToken),
                        Quantity = record.Quantity,
                        CustomerOrder = await wmsDbContext.CustomerOrders.FirstAsync(customerOrder => customerOrder.Id == record.CustomerOrderId, cancellationToken),
                        CustomerOrderId = record.CustomerOrderId,
                        
                    };
                    customerOrderDetailsToCreate.Add(customerOrderDetail);
                }

                await wmsDbContext.AddRangeAsync(customerOrderDetailsToCreate, cancellationToken);
                await wmsDbContext.SaveChangesAsync(cancellationToken);

                var customerOrderDetailsCreated = new List<CreateCustomerOrderDetailResponseDTO>();
                foreach (var customerOrderDetailToCreate in customerOrderDetailsToCreate)
                {
                    var createCustomerOrderDetailResponseDTO = new CreateCustomerOrderDetailResponseDTO()
                    {
                        Id = customerOrderDetailToCreate.Id,
                        CreatedBy = customerOrderDetailToCreate.CreatedBy,
                        Status = customerOrderDetailToCreate.Status,
                        ProductId = customerOrderDetailToCreate.ProductId,
                        Quantity = customerOrderDetailToCreate.Quantity,
                        CustomerOrderId = customerOrderDetailToCreate.CustomerOrderId,
                        
                    };
                    customerOrderDetailsCreated.Add(createCustomerOrderDetailResponseDTO);
                }

                return customerOrderDetailsCreated;
            }
            catch (Exception exception)
            {
                return new List<CreateCustomerOrderDetailResponseDTO>();
            }
        }
    }
}
