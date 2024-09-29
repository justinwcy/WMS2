using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteRackCommand(Guid Id) : IRequest<ServiceResponse>;
}
    