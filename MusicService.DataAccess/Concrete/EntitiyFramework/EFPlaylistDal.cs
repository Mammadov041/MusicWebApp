using MusicService.DataAccess.Abstract;
using MusicService.Entities.Entities;
using MusicWebApp.Core.DataAccess.EntityFramework;

namespace MusicService.DataAccess.Concrete.EntitiyFramework
{
    public class EFPlaylistDal : EFEntityRepositoryBase<Playlist,MusicServiceDbContext>,IPlaylistDal
    {
        public EFPlaylistDal(MusicServiceDbContext context):base(context) { }
    }
}
