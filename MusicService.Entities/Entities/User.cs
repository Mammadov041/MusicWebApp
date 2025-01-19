using Microsoft.AspNetCore.Identity;
using MusicWebApp.Core.Abstract;

namespace MusicService.Entities.Entities
{
    public class User : IdentityUser,IEntity
    {
        // Navigation properties 
        public virtual ICollection<Playlist>? Playlists { get; set; }
        public virtual ICollection<Music>? PublishedMusics { get; set; }
    }
}
