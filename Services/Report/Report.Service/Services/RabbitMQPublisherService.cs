using RabbitMQ.Client;
using Report.Service.Models;
using System.Text;
using System.Text.Json;

namespace Report.Service.Services
{
    public class RabbitMQPublisherService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisherService(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish(CreateReportEvent createReportEvent)
        {
            var channel = _rabbitMQClientService.Connect();
            var bodyString = JsonSerializer.Serialize(createReportEvent);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(RabbitMQClientService.ExchangeName, RabbitMQClientService.RoutingReport, properties, bodyByte);

        }
    }
}
