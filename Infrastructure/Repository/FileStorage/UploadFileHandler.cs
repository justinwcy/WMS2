using Application.Interface;
using Application.Service.Commands;

using MediatR;

namespace Infrastructure.Repository
{
    public class UploadFileHandler(IFileStorage fileStorage) : 
        IRequestHandler<UploadFileCommand, string>
    {
        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            return await fileStorage.UploadFileAsync(request.FileStream, request.FileName);

        }
    }
}
