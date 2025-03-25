using Application.Service.Queries;
using MediatR;
using WebUI.Components.Models;

namespace WebUI.Utilities
{
    public static class GetAllUtilities
    {
        public static async Task<List<WarehouseModel>> GetAllWarehouses(
            IMediator mediator,
            Guid companyId)
        {
            var getAllWarehouseIdsByCompanyIdQuery = new GetAllWarehouseIdsByCompanyIdQuery(companyId);
            var warehouseIds = await mediator.Send(getAllWarehouseIdsByCompanyIdQuery);
            var getWarehousesByIdsQuery = new GetWarehousesByIdsQuery(warehouseIds);
            var getWarehouseResponseDTOs = await mediator.Send(getWarehousesByIdsQuery);
            var warehouseModels = getWarehouseResponseDTOs.Select(getWarehouseResponseDTO =>
                new WarehouseModel()
                {
                    Id = getWarehouseResponseDTO.Id,
                    Name = getWarehouseResponseDTO.Name,
                    Address = getWarehouseResponseDTO.Address,
                    ZoneIds = getWarehouseResponseDTO.ZoneIds,
                    CompanyId = getWarehouseResponseDTO.CompanyId,
                }).ToList();

            return warehouseModels;
        }
    }
}
