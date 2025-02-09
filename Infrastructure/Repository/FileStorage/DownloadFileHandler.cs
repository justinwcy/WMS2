using Application.Interface;
using Application.Service.Queries;

using MediatR;

namespace Infrastructure.Repository
{
    public class DownloadFileHandler(IFileStorage fileStorage) : 
        IRequestHandler<DownloadFileQuery, Stream?>
    {
        public async Task<Stream?> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            return await fileStorage.DownloadFileAsync(request.FileName);

        }
    }
}
