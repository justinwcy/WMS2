using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteShopCommand(Guid Id) : IRequest<ServiceResponse>;
}
    