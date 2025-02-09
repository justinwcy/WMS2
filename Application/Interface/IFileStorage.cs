namespace Application.Interface
{
    public interface IFileStorage
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
        Task<Stream?> DownloadFileAsync(string fileName); // Or fileName if you store paths separately
        Task<bool> IsFileExist(string fileName); // Or filePath
        Task<bool> DeleteFileAsync(string fileName); // Or filePath
    }
}
