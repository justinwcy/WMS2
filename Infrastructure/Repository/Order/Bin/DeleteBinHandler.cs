using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteBinHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteBinCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteBinCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundBin = await wmsDbContext.Bins.FirstOrDefaultAsync(
                    bin => bin.Id == request.Id,
                    cancellationToken);
                if (foundBin == null)
                {
                    return GeneralDbResponses.ItemNotFound("Bin");
                }

                wmsDbContext.Bins.Remove(foundBin);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("Bin");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    