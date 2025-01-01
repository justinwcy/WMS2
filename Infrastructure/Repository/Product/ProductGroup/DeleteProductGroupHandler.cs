using Application.DTO.Response;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteProductGroupHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<DeleteProductGroupCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteProductGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundProductGroup = await wmsDbContext.ProductGroups.FirstOrDefaultAsync(
                    productGroup => productGroup.Id == request.Id,
                    cancellationToken);
                if (foundProductGroup == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductGroup");
                }

                wmsDbContext.ProductGroups.Remove(foundProductGroup);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemDeleted("ProductGroup");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    