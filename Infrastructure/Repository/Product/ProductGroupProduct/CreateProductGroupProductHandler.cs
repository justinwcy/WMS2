using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateProductGroupProductHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateProductGroupProductCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(CreateProductGroupProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                
                var data = request.Model.Adapt<ProductGroupProduct>();
                wmsDbContext.ProductGroupProducts.Add(data);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemCreated("ProductGroupProduct");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    