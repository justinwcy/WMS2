using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<ServiceResponse>;
}
    