using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetInventoriesByIdsQuery(List<Guid> InventoryIds) : IRequest<List<GetInventoryResponseDTO>>;
}
