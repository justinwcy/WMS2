using Application.DTO.Response;
using Application.Interface;
using Application.Service.Commands;

using MediatR;

namespace Infrastructure.Repository
{
    public class DeleteFileHandler(IFileStorage fileStorage) : 
        IRequestHandler<DeleteFileCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var success = await fileStorage.DeleteFileAsync(request.FileName);
                return new ServiceResponse(success, $"Successfully deleted {request.FileName}");
            }
            catch (Exception exception) 
            {
                return new ServiceResponse(false, 
                    $"Failed to delete {request.FileName}: {exception.Message}");
            }
        }
    }
}
