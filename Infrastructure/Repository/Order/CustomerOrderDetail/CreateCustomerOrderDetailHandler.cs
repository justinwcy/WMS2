using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateCustomerOrderDetailHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateCustomerOrderDetailCommand, CreateCustomerOrderDetailResponseDTO>
    {
        public async Task<CreateCustomerOrderDetailResponseDTO> Handle(CreateCustomerOrderDetailCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<CustomerOrderDetail>();
            var customerOrderDetailCreated = wmsDbContext.CustomerOrderDetails.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateCustomerOrderDetailResponseDTO() { Id = customerOrderDetailCreated.Entity.Id};
        }
    }
}
    