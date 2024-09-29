using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteIncomingOrderProductCommand(Guid Id) : IRequest<ServiceResponse>;
}
