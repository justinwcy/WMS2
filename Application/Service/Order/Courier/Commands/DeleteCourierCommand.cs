using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteCourierCommand(Guid Id) : IRequest<ServiceResponse>;
}
    