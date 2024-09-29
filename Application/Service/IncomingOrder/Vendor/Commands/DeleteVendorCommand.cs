using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteVendorCommand(Guid Id) : IRequest<ServiceResponse>;
}
