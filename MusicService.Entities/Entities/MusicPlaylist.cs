using MusicWebApp.Core.Abstract;

namespace MusicService.Entities.Entities
{
    public class MusicPlaylist : IEntity
    {
        public int Id { get; set; }

        // Foreign keys :
        public int MusicId { get; set; }
        public int PlaylistId { get; set; }

        // Navigation properties :
        public virtual Music Music { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}
