using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

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
                var binFound = await wmsDbContext.Bins.FirstOrDefaultAsync(
                    bin => bin.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (binFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("Bin");
                }

                wmsDbContext.Entry(binFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<Bin>();
                wmsDbContext.Bins.Update(adaptData);
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
    