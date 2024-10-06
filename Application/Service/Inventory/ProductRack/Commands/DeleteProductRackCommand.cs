using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteProductRackCommand(Guid Id) : IRequest<ServiceResponse>;
}
    