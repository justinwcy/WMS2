using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateRackHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateRackCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateRackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var rackFound = await wmsDbContext.Racks.FirstOrDefaultAsync(
                    rack => rack.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (rackFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Rack");
                }

                rackFound.Name = request.Model.Name;
                rackFound.Depth = request.Model.Depth;
                rackFound.Height = request.Model.Height;
                rackFound.MaxWeight = request.Model.MaxWeight;
                rackFound.Width = request.Model.Width;

                var productsToAdd = await wmsDbContext.Products
                    .Where(product => request.Model.ProductIds.Contains(product.Id))
                    .ToListAsync(cancellationToken);
                rackFound.Products.RemoveAll(product => !request.Model.ProductIds.Contains(product.Id));
                foreach (var productToAdd in productsToAdd)
                {
                    if (rackFound.Products.All(product => product.Id != productToAdd.Id))
                    {
                        rackFound.Products.Add(productToAdd);
                    }
                }

                var zoneFound = await wmsDbContext.Zones.FirstOrDefaultAsync(
                    zone => zone.Id == request.Model.ZoneId,
                    cancellationToken);
                if (zoneFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Zone");
                }
                rackFound.Zone = zoneFound;
                rackFound.ZoneId = request.Model.ZoneId;

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Rack");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    