using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Report.Service.Dtos;
using Report.Service.Models;
using Shared.Dtos;
using System.Text;
using System.Text.Json;

namespace Report.Service.Services
{
    public class ExcelReportBackgroundService:BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly HttpClientService _httpClientService;
        private readonly IReportService _reportService;
        private IModel _channel;
        public static string ReportDataUrl = @"http://localhost:5235/api/Contact/GetAllContactsWithCommunications";

        public ExcelReportBackgroundService(RabbitMQClientService rabbitMQClientService, HttpClientService httpClientService, IReportService reportService)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _httpClientService = httpClientService;
            _reportService = reportService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0,1,false);   
            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer =  new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }

        private Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var reportEvent = JsonSerializer.Deserialize<CreateReportEvent>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            var rawData = _httpClientService.GetAsync(ReportDataUrl).Result.Data;
            var contacts = JsonSerializer.Deserialize<List<ContactWithCommunicationsDto>>(rawData);
            List<ReportDataDto> reportDatas = new List<ReportDataDto>();
            CreateReportData(contacts, reportDatas);

            return Task.CompletedTask;

        }

        private static void CreateReportData(List<ContactWithCommunicationsDto> contacts, List<ReportDataDto> reportDatas)
        {
            var locations = contacts.SelectMany(x => x.Communications)
                            .Where(y => y.CommunicationType == CommunicationType.LOCATION)
                            .Select(z => z.Address).Distinct().ToList();


            foreach (var locationName in locations)
            {
                var contactsFromLocation = contacts.Where(x => x.Communications.Any(y => y.Address == locationName && y.CommunicationType == CommunicationType.LOCATION));
                var phoneNumberCount = contactsFromLocation.SelectMany(x => x.Communications).Where(y => y.CommunicationType == CommunicationType.PHONE).Count();
                reportDatas.Add(new ReportDataDto
                {
                    Location = locationName,
                    ContactCount = contactsFromLocation.Count(),
                    PhoneNumberCount = phoneNumberCount
                });
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
