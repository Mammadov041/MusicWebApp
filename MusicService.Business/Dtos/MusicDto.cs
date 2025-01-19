using Microsoft.AspNetCore.Http;

namespace MusicWebApp.MusicService.Dtos
{
    public class MusicDto
    {
        public string? SingerName { get; set; }
        public string? Name { get; set; }
        public IFormFile? ImageFile { get; set; }
        public IFormFile? AudioFile { get; set; }
        public string AuthorId { get; set; }
    }
}
