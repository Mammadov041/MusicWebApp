using MusicService.DataAccess.Abstract;
using MusicService.Entities.Entities;
using MusicWebApp.Core.DataAccess.EntityFramework;

namespace MusicService.DataAccess.Concrete.EntitiyFramework
{
    public class EFMusicPlaylistDal : EFEntityRepositoryBase<MusicPlaylist,MusicServiceDbContext>,IMusicPlaylistDal
    {
        public EFMusicPlaylistDal(MusicServiceDbContext context) : base(context) { }
    }
}
