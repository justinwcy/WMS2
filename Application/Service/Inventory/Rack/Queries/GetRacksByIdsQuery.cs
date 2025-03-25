using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetRacksByIdsQuery(List<Guid> RackIds) : IRequest<List<GetRackResponseDTO>>;
}
