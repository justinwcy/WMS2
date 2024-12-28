using Application.DTO.Request;
using Application.DTO.Request.Identity;
using Application.DTO.Response;

using MediatR;

namespace Application.Service.Commands
{
    public record LoginStaffCommand(LoginStaffRequestDTO Model) : IRequest<ServiceResponse>;
}
