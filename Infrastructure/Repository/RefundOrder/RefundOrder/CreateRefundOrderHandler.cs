using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateRefundOrderHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateRefundOrderCommand, CreateRefundOrderResponseDTO>
    {
        public async Task<CreateRefundOrderResponseDTO> Handle(CreateRefundOrderCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<RefundOrder>();
            var refundOrderCreated = wmsDbContext.RefundOrders.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateRefundOrderResponseDTO() { Id = refundOrderCreated.Entity.Id};
        }
    }
}
    