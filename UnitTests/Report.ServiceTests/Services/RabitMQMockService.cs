using Report.Service.Models;
using Report.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.ServiceTests.Services
{
    public class RabitMQMockService : IRabbitMQPublisherService
    {
        public void Publish(CreateReportEvent createReportEvent, string queue, string routing, string exchange)
        {
            
        }
    }
}
