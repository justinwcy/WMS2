using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetStaffIdByEmailQuery(string StaffEmail) : IRequest<Guid>;
}
