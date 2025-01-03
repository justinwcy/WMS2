using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateRackHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateRackCommand, CreateRackResponseDTO>
    {
        public async Task<CreateRackResponseDTO> Handle(CreateRackCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<Rack>();
            var rackCreated = wmsDbContext.Racks.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateRackResponseDTO() { Id = rackCreated.Entity.Id};
        }
    }
}
    