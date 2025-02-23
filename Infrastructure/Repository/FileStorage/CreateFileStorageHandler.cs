using Application.DTO.Response;
using Application.Interface;
using Application.Service.Commands;
using Domain.Entities;
using Infrastructure.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Repository
{
    public class CreateFileStorageHandler(
        IWmsDbContextFactory<WmsDbContext> contextFactory,
        IFileStorage fileStorage) :
        IRequestHandler<CreateFileStorageCommand, CreateFileStorageResponseDTO>
    {
        public async Task<CreateFileStorageResponseDTO> Handle(CreateFileStorageCommand request, CancellationToken cancellationToken)
        {
            await using var wmsDbContext = contextFactory.CreateDbContext();
            var fileStorageFound = await wmsDbContext.FileStorages.AsNoTracking()
                .FirstOrDefaultAsync(fileStorage => fileStorage.FileLink.Contains(request.Model.FileName), cancellationToken);
            if (fileStorageFound != null)
            {
                return null;
            }

            var fileLink = await fileStorage.UploadFileAsync(request.Model.FileStream, request.Model.FileName);
            var data = new FileStorage()
            {
                CreatedBy = request.Model.CreatedBy,
                FileLink = fileLink,
                StorageType = request.Model.StorageType,
            };

            var fileStorageCreated = wmsDbContext.FileStorages.Add(data);
            await wmsDbContext.SaveChangesAsync(cancellationToken);

            var response = new CreateFileStorageResponseDTO()
            {
                Id = fileStorageCreated.Entity.Id,
            };

            return response;

        }
    }
}
