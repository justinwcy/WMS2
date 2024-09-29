using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteCustomerCommand(Guid Id) : IRequest<ServiceResponse>;
}
    