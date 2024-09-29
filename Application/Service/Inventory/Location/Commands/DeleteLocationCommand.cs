using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteLocationCommand(Guid Id) : IRequest<ServiceResponse>;
}
    