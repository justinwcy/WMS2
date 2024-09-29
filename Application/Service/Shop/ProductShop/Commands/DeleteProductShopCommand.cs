using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteProductShopCommand(Guid Id) : IRequest<ServiceResponse>;
}
    