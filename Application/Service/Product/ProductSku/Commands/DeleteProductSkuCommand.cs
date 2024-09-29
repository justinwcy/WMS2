using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteProductSkuCommand(Guid Id) : IRequest<ServiceResponse>;
}
    