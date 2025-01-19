using MusicService.DataAccess.Abstract;
using MusicService.Entities.Entities;
using MusicWebApp.Core.DataAccess.EntityFramework;

namespace MusicService.DataAccess.Concrete.EntitiyFramework
{
    public class EFMusicDal : EFEntityRepositoryBase<Music, MusicServiceDbContext>,IMusicDal
    {
        public EFMusicDal(MusicServiceDbContext context) : base(context) { }
    }
}
