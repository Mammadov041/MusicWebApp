using MusicService.Business.Abstract;
using MusicService.DataAccess.Abstract;
using MusicService.Entities.Entities;

namespace MusicService.Business.Concrete
{
    public class MusicPlaylistService : IMusicPlaylistService
    {
        private readonly IMusicPlaylistDal _musicPlaylistDal;
        public MusicPlaylistService(IMusicPlaylistDal musicPlaylistDal)
        {
            _musicPlaylistDal = musicPlaylistDal;
        }

        public async Task AddMusicPlaylistAysnc(MusicPlaylist musicPlaylist)
        {
            await _musicPlaylistDal.AddAsync(musicPlaylist);
        }

        public async Task RemoveMusicPlaylistAsync(int id)
        {
            var musicPlaylist = await _musicPlaylistDal.GetAsync(mp => mp.Id == id);
            await _musicPlaylistDal.DeleteAsync(musicPlaylist);
        }
    }
}
