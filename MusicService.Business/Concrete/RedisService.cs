using MusicService.Business.Abstract;
using StackExchange.Redis;

namespace MusicService.Business.Concrete
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _db;

        public RedisService(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task AddLikedMusicAsync(string userId, int musicId)
        {
            var key = $"user:{userId}:liked_musics";
            await _db.SetAddAsync(key, musicId);
        }

        public async Task<IEnumerable<string>> GetLikedMusicsAsync(string userId)
        {
            var key = $"user:{userId}:liked_musics";
            var songs = await _db.SetMembersAsync(key);
            return songs.Select(songId => songId.ToString());
        }

        public async Task RemoveLikedMusicAsync(string userId, int musicId)
        {
            var key = $"user:{userId}:liked_musics";
            await _db.SetRemoveAsync(key, musicId);
        }
    }
}
