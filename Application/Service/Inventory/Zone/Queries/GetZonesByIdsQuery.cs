using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetZonesByIdsQuery(List<Guid> ZoneIds) : IRequest<List<GetZoneResponseDTO>>;
}
