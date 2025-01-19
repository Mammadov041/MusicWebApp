using MusicService.Business.Abstract;
using MusicService.DataAccess.Abstract;
using MusicService.Entities.Entities;

namespace MusicService.Business.Concrete
{
    public class PlaylistService : IPlayListService
    {
        private readonly IPlaylistDal _playlistDal;

        public PlaylistService(IPlaylistDal playlistDal)
        {
            _playlistDal = playlistDal;
        }

        public async Task AddPlaylistAsync(Playlist playlist)
        {
            await _playlistDal.AddAsync(playlist);
        }

        public async Task RemovePlaylistAsync(int id)
        {
            var playlist = await _playlistDal.GetAsync(p => p.Id == id);
            await _playlistDal.DeleteAsync(playlist);
        }
    }
}
