using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetZoneStaffById(Guid Id) : IRequest<GetZoneStaffResponseDTO>;
}
    