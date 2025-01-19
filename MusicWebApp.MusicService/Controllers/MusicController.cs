using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicService.Business.Abstract;
using MusicService.Entities.Dtos;
using MusicWebApp.MusicService.Dtos;
using RabbitMQ.Client;

namespace MusicWebApp.MusicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IS3Service _awsService;
        private readonly IMusicService _musicService;
        private readonly IPlayListService _playListService;
        private readonly IMusicPlaylistService _musicPlaylistService;
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IRedisService _redisService;

        public MusicController(IS3Service awsService, IMusicService musicService, IPlayListService playListService, IMusicPlaylistService musicPlaylistService, IRabbitMQService rabbitMQService, IRedisService redisService)
        {
            _awsService = awsService;
            _musicService = musicService;
            _playListService = playListService;
            _musicPlaylistService = musicPlaylistService;
            _rabbitMQService = rabbitMQService;
            _redisService = redisService;
        }

        [Authorize]
        [HttpGet("all-musics")]
        public async Task<IActionResult> GetAllMusics()
        {
            var allMusics = await _musicService.GetAllMusicsAsync();
            return Ok(allMusics);
        }

        [Authorize]
        [HttpPost("upload-music")]
        public async Task<IActionResult> UploadMusic([FromForm] MusicDto musicDto)
        {
            await _musicService.AddMusicAsync(musicDto);
            return Ok("Music Added succesfully .");
        }

        [Authorize]
        [HttpPost("like-music")]
        public async Task<IActionResult> LikeMusic(string userId,int musicId)
        {
            await _redisService.AddLikedMusicAsync(userId, musicId);
            return Ok("Music liked and saved to redis sueccesfully .");
        }

        [Authorize]
        [HttpPost("unlike-music")]
        public async Task<IActionResult> UnlikeMusic(string userId, int musicId)
        {
            await _redisService.RemoveLikedMusicAsync(userId,musicId);
            return Ok("Music unliked and removed from redis succesfully .");
        }

        [Authorize]
        [HttpGet("liked-musics")]
        public async Task<IActionResult> LikedMusics(string userId)
        {
            var likedMusics = await _redisService.GetLikedMusicsAsync(userId);
            return Ok(likedMusics);
        }

        [Authorize]
        [HttpPost("comment-music")]
        public async Task<IActionResult> CommentOnMusicPost([FromBody] CommentDto commentDto)
        {
            if (commentDto == null || string.IsNullOrWhiteSpace(commentDto.Comment))
            {
                return BadRequest("Invalid comment data.");
            }

            await _rabbitMQService.SendMessageToQueueAsync($"Music:{commentDto.MusicId}", commentDto.Comment);
            return Ok("Comment successfully sent to RabbitMQ.");
        }

        [Authorize]
        [HttpGet("music-comments")]
        public async Task<IActionResult> MusicComments(int musicId)
        {
            var comments = await _rabbitMQService.GetMessagesFromQueueAsync(string.Concat("Music:", musicId.ToString()));
            return Ok(comments);
        }
    }
}
