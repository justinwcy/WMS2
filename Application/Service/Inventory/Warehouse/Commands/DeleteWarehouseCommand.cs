using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteWarehouseCommand(Guid Id) : IRequest<ServiceResponse>;
}
    