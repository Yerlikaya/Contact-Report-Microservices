using Microsoft.Extensions.Options;
using Report.Service.Models;

namespace Report.Service
{
    public class Constant
    {
        #region Report API
        public const string ReportUpdateUrl = "api/Reports";
        #endregion

        #region Contact API
        public const string ContactGetReportData = "api/Contact/GetContactStatistics";
        #endregion

        #region RabitMQ 
        public const string ReportExchange = "report.direct.exchange";
        public const string ReportRouting = "report.route";
        public const string ReportQueue = "report.queue";
        #endregion
    }
}
