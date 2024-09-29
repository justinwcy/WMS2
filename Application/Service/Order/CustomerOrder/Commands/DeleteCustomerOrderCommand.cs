using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteCustomerOrderCommand(Guid Id) : IRequest<ServiceResponse>;
}
    