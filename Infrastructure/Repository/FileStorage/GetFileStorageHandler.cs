using Application.DTO.Response;
using Application.Interface;
using Application.Service.Queries;

using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GetFileStorageHandler(IWmsDbContextFactory<WmsDbContext> contextFactory,
        IFileStorage fileStorage) : 
        IRequestHandler<GetFileStorageQuery, GetFileStorageResponseDTO>
    {
        public async Task<GetFileStorageResponseDTO> Handle(
            GetFileStorageQuery request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var fileStorageFound = await wmsDbContext.FileStorages.AsNoTracking()
                .FirstAsync(fileStorageItem => fileStorageItem.Id == request.FileStorageId, cancellationToken);

            var stream = await fileStorage.DownloadFileAsync(fileStorageFound.FileLink);

            var response = new GetFileStorageResponseDTO()
            {
                CreatedBy = fileStorageFound.CreatedBy,
                FileLink = fileStorageFound.FileLink,
                FileStream = stream,
                Id = fileStorageFound.Id,
                StorageType = fileStorageFound.StorageType,
            };

            return response;

        }
    }
}
