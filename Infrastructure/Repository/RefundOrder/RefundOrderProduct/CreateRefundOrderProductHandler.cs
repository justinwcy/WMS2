using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateRefundOrderProductHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateRefundOrderProductCommand, CreateRefundOrderProductResponseDTO>
    {
        public async Task<CreateRefundOrderProductResponseDTO> Handle(CreateRefundOrderProductCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<RefundOrderProduct>();
            var refundOrderProductCreated = wmsDbContext.RefundOrderProducts.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateRefundOrderProductResponseDTO() { Id = refundOrderProductCreated.Entity.Id};
        }
    }
}
    