using MediatR;

namespace Application.Service.Queries
{
    public record DownloadFileQuery(string FileName) : IRequest<Stream?>;
}
