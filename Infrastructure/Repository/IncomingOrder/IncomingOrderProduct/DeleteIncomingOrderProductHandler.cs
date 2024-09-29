using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteIncomingOrderProductHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteIncomingOrderProductCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteIncomingOrderProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundIncomingOrderProduct = await wmsDbContext.IncomingOrderProducts.FirstOrDefaultAsync(
                    incomingOrderProduct => incomingOrderProduct.Id == request.Id,
                    cancellationToken);
                if (foundIncomingOrderProduct == null)
                {
                    return GeneralDbResponses.ItemNotFound("IncomingOrderProduct");
                }

                wmsDbContext.IncomingOrderProducts.Remove(foundIncomingOrderProduct);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("IncomingOrderProduct");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    