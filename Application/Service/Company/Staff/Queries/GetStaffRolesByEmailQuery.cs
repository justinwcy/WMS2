using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetStaffRolesByEmailQuery(string StaffEmail) : IRequest<IEnumerable<string>>;
}
