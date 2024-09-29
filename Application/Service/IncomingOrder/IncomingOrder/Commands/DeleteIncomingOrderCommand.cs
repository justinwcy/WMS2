using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteIncomingOrderCommand(Guid Id) : IRequest<ServiceResponse>;
}
