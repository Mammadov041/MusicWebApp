using MusicService.Business.Abstract;
using MusicService.DataAccess.Abstract;
using MusicService.Entities.Entities;
using MusicWebApp.MusicService.Dtos;

namespace MusicService.Business.Concrete
{
    public class MusicService_ : IMusicService
    {
        private readonly IMusicDal _musicDal;
        private readonly IS3Service _awsService;

        public MusicService_(IMusicDal musicDal, IS3Service awsService)
        {
            _musicDal = musicDal;
            _awsService = awsService;
        }

        public async Task AddMusicAsync(MusicDto dto)
        {
            var musicImageUrl = await _awsService.UploadFileAsync(dto.ImageFile.FileName,dto.ImageFile.OpenReadStream(),dto.ImageFile.ContentType);
            var musicAudioUrl = await _awsService.UploadFileAsync(dto.AudioFile.FileName, dto.AudioFile.OpenReadStream(), dto.AudioFile.ContentType);
            var music = new Music
            {
                AudioPath = musicAudioUrl,
                ImagePath = musicImageUrl,
                SingerName = dto.SingerName,
                AuthorId = dto.AuthorId,
                Name = dto.Name,
            };
            await _musicDal.AddAsync(music);
        }

        public async Task RemoveMusicAsync(int id)
        {
            var music = await _musicDal.GetAsync(m => m.Id == id);
            await _musicDal.DeleteAsync(music);
        }

        public async Task<List<Music>> GetAllMusicsAsync()
        {
            var allMusics = await _musicDal.GetListAsync();
            return allMusics;
        }
    }
}
