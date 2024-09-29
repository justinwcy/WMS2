using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

namespace Infrastructure.Repository
{
    public class CreateCustomerOrderDetailHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : 
        IRequestHandler<CreateCustomerOrderDetailCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(CreateCustomerOrderDetailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                
                var data = request.Model.Adapt<CustomerOrderDetail>();
                wmsDbContext.CustomerOrderDetails.Add(data);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemCreated("CustomerOrderDetail");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    