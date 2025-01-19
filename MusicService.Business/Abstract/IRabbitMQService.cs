namespace MusicService.Business.Abstract
{
    public interface IRabbitMQService
    {
        Task SendMessageToQueueAsync(string queueName, string message);
        Task<List<string>> GetMessagesFromQueueAsync(string queueName);
    }
}
