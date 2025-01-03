using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateCustomerOrderHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateCustomerOrderCommand, CreateCustomerOrderResponseDTO>
    {
        public async Task<CreateCustomerOrderResponseDTO> Handle(CreateCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<CustomerOrder>();
            var customerOrderCreated = wmsDbContext.CustomerOrders.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateCustomerOrderResponseDTO() { Id = customerOrderCreated.Entity.Id};
        }
    }
}
    