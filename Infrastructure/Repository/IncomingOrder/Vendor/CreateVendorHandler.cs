using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateVendorHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateVendorCommand, CreateVendorResponseDTO>
    {
        public async Task<CreateVendorResponseDTO> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            
            var data = request.Model.Adapt<Vendor>();
            var vendorCreated = wmsDbContext.Vendors.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);
            return new CreateVendorResponseDTO() { Id = vendorCreated.Entity.Id};
        }
    }
}
    