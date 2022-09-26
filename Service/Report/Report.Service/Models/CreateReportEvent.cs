namespace Report.Service.Models
{
    public class CreateReportEvent
    {
        public string ReportName { get; set; }

        public static string GetReportName(DateTime dateTime)
        {
            var strDateTime = dateTime.ToString("yyyy-MM-dd-HH-mm-ss");
            return string.Format("{0}_{1}",strDateTime, Guid.NewGuid().ToString());
        }
    }
}
