using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteStaffCommand(Guid staffId) : IRequest<ServiceResponse>;
}
