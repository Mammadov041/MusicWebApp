namespace MusicService.Business.Abstract
{
    public interface IS3Service
    {
        Task<string> UploadFileAsync(string fileName, Stream fileStream, string contentType);
    }
}
