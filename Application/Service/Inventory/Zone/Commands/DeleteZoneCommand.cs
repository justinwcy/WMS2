using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteZoneCommand(Guid Id) : IRequest<ServiceResponse>;
}
    