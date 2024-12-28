using Application.DTO.Request.Identity;
using Application.DTO.Response;

using MediatR;

namespace Application.Service.Commands
{
    public record CheckLoginStaffCommand(CheckLoginStaffRequestDTO Model) : IRequest<ServiceResponse>;
}
