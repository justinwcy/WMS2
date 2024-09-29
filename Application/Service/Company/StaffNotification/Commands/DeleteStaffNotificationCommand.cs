using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteStaffNotificationCommand(Guid staffNotificationId) : IRequest<ServiceResponse>;
}
