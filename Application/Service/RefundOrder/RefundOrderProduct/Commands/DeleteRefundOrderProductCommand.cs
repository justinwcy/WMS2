using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteRefundOrderProductCommand(Guid Id) : IRequest<ServiceResponse>;
}
    