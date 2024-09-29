using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteInventoryCommand(Guid Id) : IRequest<ServiceResponse>;
}
    