using Report.Service.Dtos;
using Report.Service.Models;
using Shared.Dtos;

namespace Report.Service.Services
{
    public interface IRabbitMQPublisherService
    {
        void Publish(CreateReportEvent createReportEvent, string queue, string routing, string exchange);
    }
}
