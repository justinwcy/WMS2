using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetWarehousesByIdsQuery(List<Guid> WarehouseIds) : IRequest<List<GetWarehouseResponseDTO>>;
}
