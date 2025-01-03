using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteProductGroupProductCommand(Guid Id) : IRequest<ServiceResponse>;
}
    