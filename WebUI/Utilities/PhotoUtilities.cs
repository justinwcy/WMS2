using Application.Service.Queries;
using MediatR;

using Microsoft.AspNetCore.Components.Forms;

namespace WebUI.Utilities
{
    public static class PhotoUtilities
    {
        public static async Task<string> GetPhotoBase64String(
            IMediator mediator,
            Guid photoId)
        {
            var getFileStorageRequestDTO = new GetFileStorageQuery(photoId);
            var getFileStorageResponseDTO = await mediator.Send(getFileStorageRequestDTO);
            return await ConvertToBase64String(getFileStorageResponseDTO.FileStream);
        }

        public static async Task<string> ConvertToBase64String(IBrowserFile file)
        {
            var memoryStream = new MemoryStream();
            await file.OpenReadStream(file.Size).CopyToAsync(memoryStream);

            return await ConvertToBase64String(memoryStream);
        }

        public static async Task<string> ConvertToBase64String(Stream stream)
        {
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            return await ConvertToBase64String(memoryStream);
        }

        private static async Task<string> ConvertToBase64String(MemoryStream memoryStream)
        {
            var bytes = new byte[memoryStream.Length];
            memoryStream.Position = 0;
            await memoryStream.ReadAsync(bytes);
            memoryStream.Close();
            return Convert.ToBase64String(bytes);
        }
    }
}
