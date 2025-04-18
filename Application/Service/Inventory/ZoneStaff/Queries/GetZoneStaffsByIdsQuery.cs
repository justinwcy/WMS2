using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetZoneStaffsByIdsQuery(List<Guid> ZoneStaffIds) : IRequest<List<GetZoneStaffResponseDTO>>;
}
