namespace MusicService.Business.Abstract
{
    public interface IRedisService
    {
        Task AddLikedMusicAsync(string userId,int musicId);
        Task<IEnumerable<string>> GetLikedMusicsAsync(string userId);
        Task RemoveLikedMusicAsync(string userId,int musicId);
    }
}
