using MusicWebApp.Core.Abstract;

namespace MusicService.Entities.Entities
{
    public class Playlist : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string UserId { get; set; }

        // Navigation properties 
        public virtual ICollection<Music>? Musics { get; set; }
    }
}
