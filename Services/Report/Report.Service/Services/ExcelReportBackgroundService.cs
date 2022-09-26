using ClosedXML.Excel;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Report.Service.Dtos;
using Report.Service.Models;
using Shared.Dtos;
using System.Data;
using System.Text;
using System.Text.Json;

namespace Report.Service.Services
{
    public class ExcelReportBackgroundService : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly HttpClientService _httpClientService;
        private IModel _channel;
        public static string ReportDataGetUrl = @"http://localhost:5235/api/Contact/GetAllContactsWithCommunications";
        public static string ReportUpdateUrl = @"http://localhost:5170/api/Reports";

        public ExcelReportBackgroundService(RabbitMQClientService rabbitMQClientService, HttpClientService httpClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _httpClientService = httpClientService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var reportEvent = JsonSerializer.Deserialize<CreateReportEvent>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwRoot", reportEvent.ReportName +".xlsx");
            try
            {
                _= UpdateReportInformationsAsync(reportEvent, path, ReportStatusType.INPROGRESS);
                var reportDataRaw = await _httpClientService.GetAsync(ReportDataGetUrl);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var contactsResponse = JsonSerializer.Deserialize<ContactReportDataDto>(reportDataRaw.Data, options);

                List<ReportDataDto> reportDatas = new List<ReportDataDto>();
                CreateReportData(contactsResponse.Data, reportDatas);
                CreateExcel(reportEvent, reportDatas, path);

                Task.Delay(5000).Wait();// Yapay kuyruk trafiği...

                _= UpdateReportInformationsAsync(reportEvent, path, ReportStatusType.COMPLETED);

                _channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _= UpdateReportInformationsAsync(reportEvent, path, ReportStatusType.FAILED);
            }

        }

        private async Task UpdateReportInformationsAsync(CreateReportEvent reportEvent, string path, ReportStatusType reportStatusType)
        {
            StringContent stringContent = CreateUpdateRequest(reportEvent, path, reportStatusType);
            await _httpClientService.PostPutAsync(ReportUpdateUrl, stringContent, true);
        }

        private static StringContent CreateUpdateRequest(CreateReportEvent reportEvent, string path, ReportStatusType reportStatusType)
        {
            ReportDto reportDto = new ReportDto { Id = reportEvent.ReportId, CreatedDate = DateTime.UtcNow, ReportPath = path, Status = reportStatusType };
            var jsonReportDto = JsonSerializer.Serialize(reportDto);
            StringContent stringContent = new StringContent(jsonReportDto, Encoding.UTF8, "application/json");
            return stringContent;
        }

        private static void CreateExcel(CreateReportEvent reportEvent,List<ReportDataDto> reportDatas, string path)
        {
            using (IXLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet("ReportWithLocations").FirstCell().InsertTable<ReportDataDto>(reportDatas,false);
                workbook.SaveAs(path);
            }
        }

        private static void CreateReportData(List<ContactWithCommunicationsDto> contacts, List<ReportDataDto> reportDatas)
        {
            var locations = contacts.SelectMany(x => x.Communications)
                            .Where(y => y?.CommunicationType == CommunicationType.LOCATION)
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
