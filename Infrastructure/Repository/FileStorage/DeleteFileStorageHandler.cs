using Application.DTO.Response;
using Application.Interface;
using Application.Service.Commands;

using Infrastructure.Data;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DeleteFileStorageHandler(
        IWmsDbContextFactory<WmsDbContext> contextFactory,
        IFileStorage fileStorage) : 
        IRequestHandler<DeleteFileStorageCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(DeleteFileStorageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var foundFileStorage = await wmsDbContext.FileStorages
                    .FirstAsync(fileStorage => fileStorage.FileLink == request.FileName, cancellationToken: cancellationToken);
                if (foundFileStorage == null)
                {
                    return GeneralDbResponses.ItemNotFound(request.FileName);
                }

                var success = await fileStorage.DeleteFileAsync(request.FileName);

                if (success)
                {
                    wmsDbContext.FileStorages.Remove(foundFileStorage);
                    await wmsDbContext.SaveChangesAsync(cancellationToken);
                    return GeneralDbResponses.ItemDeleted(request.FileName);
                }

                return new ServiceResponse(false, $"Something went wrong deleting the file");

            }
            catch (Exception exception) 
            {
                return new ServiceResponse(false, 
                    $"Failed to delete {request.FileName}: {exception.Message}");
            }
        }
    }
}
