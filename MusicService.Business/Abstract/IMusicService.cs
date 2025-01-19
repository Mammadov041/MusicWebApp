using MusicService.Entities.Entities;
using MusicWebApp.MusicService.Dtos;

namespace MusicService.Business.Abstract
{
    public interface IMusicService
    {
        Task AddMusicAsync(MusicDto music);
        Task RemoveMusicAsync(int id);
        Task<List<Music>> GetAllMusicsAsync();
    }
}
