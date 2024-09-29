using Application.DTO.Response;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateCustomerOrderDetailHandler(IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateCustomerOrderDetailCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateCustomerOrderDetailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var customerOrderDetailFound = await wmsDbContext.CustomerOrderDetails.FirstOrDefaultAsync(
                    customerOrderDetail => customerOrderDetail.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (customerOrderDetailFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("CustomerOrderDetail");
                }

                wmsDbContext.Entry(customerOrderDetailFound).State = EntityState.Detached;
                var adaptData = request.Model.Adapt<CustomerOrderDetail>();
                wmsDbContext.CustomerOrderDetails.Update(adaptData);
                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("CustomerOrderDetail");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    