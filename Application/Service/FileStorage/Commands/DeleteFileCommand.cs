using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteFileCommand(string FileName) : IRequest<ServiceResponse>;
}
