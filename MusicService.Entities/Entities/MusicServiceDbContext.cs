using Microsoft.EntityFrameworkCore;

namespace MusicService.Entities.Entities
{
    public class MusicServiceDbContext : DbContext
    {
        public MusicServiceDbContext(DbContextOptions<MusicServiceDbContext> options): base(options) { }

        // Tables 
        public virtual DbSet<Music> Musics { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<MusicPlaylist> MusicPlaylists { get; set; }
    }
}
