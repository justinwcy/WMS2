using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetStaffsByIdsQuery(List<Guid> StaffIds) : IRequest<List<GetStaffResponseDTO>>;
}
