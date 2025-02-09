using MediatR;

namespace Application.Service.Commands
{
    public record UploadFileCommand(Stream FileStream, string FileName) : IRequest<string>;
}
    