using MusicService.Entities.Entities;

namespace MusicService.Business.Abstract
{
    public interface IPlayListService
    {
        Task AddPlaylistAsync(Playlist playlist);
        Task RemovePlaylistAsync(int id);
    }
}
