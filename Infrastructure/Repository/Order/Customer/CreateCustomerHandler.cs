using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateCustomerHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateCustomerCommand, CreateCustomerResponseDTO>
    {
        public async Task<CreateCustomerResponseDTO> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<Customer>();
            var customerCreated = wmsDbContext.Customers.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateCustomerResponseDTO() { Id = customerCreated.Entity.Id};
        }
    }
}
    