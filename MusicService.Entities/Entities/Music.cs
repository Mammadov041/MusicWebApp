using MusicWebApp.Core.Abstract;

namespace MusicService.Entities.Entities
{
    public class Music : IEntity
    {
        public int Id { get; set; }
        public string? SingerName { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public string? AudioPath { get; set; }
        public string AuthorId { get; set; }

        // Navigation properties 
        public virtual ICollection<Playlist>? Playlists { get; set; }
    }
}
