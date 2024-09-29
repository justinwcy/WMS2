using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetStaffByIdQuery(Guid staffId) : IRequest<GetStaffResponseDTO>;
}
