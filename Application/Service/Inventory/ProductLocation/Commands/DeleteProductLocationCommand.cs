using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteProductLocationCommand(Guid Id) : IRequest<ServiceResponse>;
}
    