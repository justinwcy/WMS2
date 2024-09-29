using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteCustomerOrderDetailCommand(Guid Id) : IRequest<ServiceResponse>;
}
    