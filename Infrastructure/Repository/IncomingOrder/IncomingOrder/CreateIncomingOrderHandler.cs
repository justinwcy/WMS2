using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateIncomingOrderHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateIncomingOrderCommand, CreateIncomingOrderResponseDTO>
    {
        public async Task<CreateIncomingOrderResponseDTO> Handle(CreateIncomingOrderCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<IncomingOrder>();
            var incomingOrderCreated = wmsDbContext.IncomingOrders.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateIncomingOrderResponseDTO() { Id = incomingOrderCreated.Entity.Id};
        }
    }
}
    