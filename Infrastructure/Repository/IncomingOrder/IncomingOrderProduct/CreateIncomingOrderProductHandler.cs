using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateIncomingOrderProductHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateIncomingOrderProductCommand, CreateIncomingOrderProductResponseDTO>
    {
        public async Task<CreateIncomingOrderProductResponseDTO> Handle(CreateIncomingOrderProductCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<IncomingOrderProduct>();
            var incomingOrderProductCreated = wmsDbContext.IncomingOrderProducts.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateIncomingOrderProductResponseDTO() { Id = incomingOrderProductCreated.Entity.Id};
        }
    }
}
    