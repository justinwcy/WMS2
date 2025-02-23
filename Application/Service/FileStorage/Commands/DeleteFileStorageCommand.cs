using Application.DTO.Response;
using MediatR;

namespace Application.Service.Commands
{
    public record DeleteFileStorageCommand(string FileName) : IRequest<ServiceResponse>;
}
