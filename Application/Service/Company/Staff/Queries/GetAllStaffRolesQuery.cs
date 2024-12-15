using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllStaffRolesQuery() : IRequest<IEnumerable<string>>;
}
