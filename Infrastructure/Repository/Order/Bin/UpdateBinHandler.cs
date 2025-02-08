using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateBinHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateBinCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateBinCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var binFound = await wmsDbContext.Bins
                    .Include(bin=>bin.CustomerOrders)
                    .FirstOrDefaultAsync(bin => bin.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (binFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Bin");
                }

                binFound.Name = request.Model.Name;

                var customerOrdersToAdd = await wmsDbContext.CustomerOrders
                    .Where(customerOrder => request.Model.CustomerOrderIds.Contains(customerOrder.Id))
                    .ToListAsync(cancellationToken);
                binFound.CustomerOrders.RemoveAll(customerOrder => !request.Model.CustomerOrderIds.Contains(customerOrder.Id));
                foreach (var customerOrderToAdd in customerOrdersToAdd)
                {
                    if (binFound.CustomerOrders.All(customerOrder => customerOrder.Id != customerOrderToAdd.Id))
                    {
                        binFound.CustomerOrders.Add(customerOrderToAdd);
                    }
                }

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("Bin");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    