using MusicService.Entities.Entities;
using MusicWebApp.Core.DataAccess.Abstract;

namespace MusicService.DataAccess.Abstract
{
    public interface IPlaylistDal : IEntityRepository<Playlist>
    {
    }
}
