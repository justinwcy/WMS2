using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteBinCommand(Guid Id) : IRequest<ServiceResponse>;
}
    