using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateBinHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateBinCommand, CreateBinResponseDTO>
    {
        public async Task<CreateBinResponseDTO> Handle(CreateBinCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<Bin>();
            var binCreated = wmsDbContext.Bins.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateBinResponseDTO() { Id = binCreated.Entity.Id};
        }
    }
}
    