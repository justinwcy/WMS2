using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteProductGroupCommand(Guid Id) : IRequest<ServiceResponse>;
}
    