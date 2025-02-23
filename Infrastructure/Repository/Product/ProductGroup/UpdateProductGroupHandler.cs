using Application.DTO.Response;
using Application.Interface;
using Application.Service.Commands;

using Domain.Entities;

using Infrastructure.Data;

using Mapster;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UpdateProductGroupHandler(IFileStorage fileStorage, 
        IWmsDbContextFactory<WmsDbContext> contextFactory) : IRequestHandler<UpdateProductGroupCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UpdateProductGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var wmsDbContext = contextFactory.CreateDbContext();
                var productGroupFound = await wmsDbContext.ProductGroups
                    .Include(productGroup=>productGroup.Photos)
                    .Include(productGroup=>productGroup.Products)
                    .FirstOrDefaultAsync(
                    productGroup => productGroup.Id.Equals(request.Model.Id),
                    cancellationToken);
                if (productGroupFound == null)
                {
                    return GeneralDbResponses.ItemNotFound("ProductGroup");
                }

                productGroupFound.Name = request.Model.Name;
                productGroupFound.Description = request.Model.Description;

                var productsToAdd = await wmsDbContext.Products
                    .Where(product => request.Model.ProductIds.Contains(product.Id))
                    .ToListAsync(cancellationToken);
                productGroupFound.Products.RemoveAll(product => !request.Model.ProductIds.Contains(product.Id));
                foreach (var productToAdd in productsToAdd)
                {
                    if (productGroupFound.Products.All(product => product.Id != productToAdd.Id))
                    {
                        productGroupFound.Products.Add(productToAdd);
                    }
                }

                var photosToAdd = await wmsDbContext.FileStorages
                    .Where(fileStorage => request.Model.PhotoIds.Contains(fileStorage.Id))
                    .ToListAsync(cancellationToken);
                var photosToDelete = productGroupFound.Photos.Where(photo => !request.Model.PhotoIds.Contains(photo.Id));
                foreach (var photoToDelete in photosToDelete)
                {
                    var success = await fileStorage.DeleteFileAsync(photoToDelete.FileLink);
                    if (success)
                    {
                        wmsDbContext.FileStorages.Remove(photoToDelete);
                    }
                }

                productGroupFound.Photos.RemoveAll(photo => !request.Model.PhotoIds.Contains(photo.Id));
                foreach (var photoToAdd in photosToAdd)
                {
                    if (productGroupFound.Photos.All(photo => photo.Id != photoToAdd.Id))
                    {
                        productGroupFound.Photos.Add(photoToAdd);
                    }
                }

                await wmsDbContext.SaveChangesAsync(cancellationToken);
                return GeneralDbResponses.ItemUpdated("ProductGroup");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
    