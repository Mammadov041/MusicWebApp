using MusicService.Entities.Entities;

namespace MusicService.Business.Abstract
{
    public interface IMusicPlaylistService
    {
        Task AddMusicPlaylistAysnc(MusicPlaylist musicPlaylist);
        Task RemoveMusicPlaylistAsync(int id);
    }
}
