using Microsoft.Extensions.Configuration;
using MusicService.Business.Abstract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MusicService.Business.Concrete
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IConfiguration _configuration;

        public RabbitMQService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<string>> GetMessagesFromQueueAsync(string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };
            var messages = new List<string>();
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: queueName,
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                messages.Add(message);
            };

            // Consume messages from the queue (fetch all messages in one call)
            await channel.BasicConsumeAsync(queue: queueName,
                                 autoAck: false,
                                 consumer: consumer);

            // Allow some time for messages to process
            await Task.Delay(1000);

            return messages;
        }

        public async Task SendMessageToQueueAsync(string queueName, string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);

            await Task.CompletedTask;
        }
    }
}
