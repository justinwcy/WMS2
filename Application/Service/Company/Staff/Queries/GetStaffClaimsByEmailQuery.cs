using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetStaffClaimsByEmailQuery(string StaffEmail) : IRequest<Dictionary<string, string>>;
}
