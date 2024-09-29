using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteZoneStaffCommand(Guid Id) : IRequest<ServiceResponse>;
}
    