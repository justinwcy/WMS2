using Application.Interface;

namespace Infrastructure.Extensions
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly string _basePath; // Path to your local storage directory

        public LocalFileStorage(string basePath)
        {
            _basePath = basePath;
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath); // Create if it doesn't exist
            }
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            var filePath = Path.Combine(_basePath, fileName);
            // Use File.Create for better performance with large files
            await using (var file = File.Create(filePath))
            {
                await fileStream.CopyToAsync(file);
            }
            return filePath; // Return the full local file path
        }

        public async Task<Stream?> DownloadFileAsync(string fileName)
        {
            var filePath = Path.Combine(_basePath, fileName);
            if (!await IsFileExist(fileName))
            {
                return null;
            }

            return new MemoryStream(await File.ReadAllBytesAsync(filePath));
        }

        public async Task<bool> IsFileExist(string fileName)  // Or string filePath if you store full path
        {
            var filePath = Path.Combine(_basePath, fileName);
            return File.Exists(filePath);
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_basePath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }
    }
}
