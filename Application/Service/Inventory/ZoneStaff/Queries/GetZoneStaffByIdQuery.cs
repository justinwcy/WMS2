using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetZoneStaffByIdQuery(Guid Id) : IRequest<GetZoneStaffResponseDTO>;
}
    