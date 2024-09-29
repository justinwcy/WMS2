using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteRefundOrderCommand(Guid Id) : IRequest<ServiceResponse>;
}
    