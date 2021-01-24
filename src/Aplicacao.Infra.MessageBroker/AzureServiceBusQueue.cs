using Aplicacao.Domain.Interfaces.CQRS;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aplicacao.Infra.MessageBroker
{
    public class AzureServiceBusQueue : IMediatorHandler
    {
        private readonly string _connectionString;

        private readonly IConfiguration _configuration;

        public AzureServiceBusQueue(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("AzureServiceBus");
        }

        public bool EnQueue<T>(T command, string queueName)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = false,
            };

            var message = JsonSerializer.Serialize(command, options);

            SendMessageAsync(message, queueName).Wait();

            return true;
        }

        private async Task SendMessageAsync(string message, string queueName)
        {
            var queueClient = new QueueClient(_connectionString, queueName);
            var encodedMessage = new Message(Encoding.UTF8.GetBytes(message));
            await queueClient.SendAsync(encodedMessage);
            await queueClient.CloseAsync();
        }
    }
}
