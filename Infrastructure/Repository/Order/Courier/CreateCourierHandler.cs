using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateCourierHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateCourierCommand, CreateCourierResponseDTO>
    {
        public async Task<CreateCourierResponseDTO> Handle(CreateCourierCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<Courier>();
            var courierCreated = wmsDbContext.Couriers.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateCourierResponseDTO() { Id = courierCreated.Entity.Id};
        }
    }
}
    