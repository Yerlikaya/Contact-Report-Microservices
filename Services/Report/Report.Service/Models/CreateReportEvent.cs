namespace Report.Service.Models
{
    public class CreateReportEvent
    {
        public CreateReportEvent()
        {
        }
        public CreateReportEvent(int id)
        {
            ReportId = id;
            var strDateTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            ReportName = string.Format("{0}_{1}", strDateTime, Guid.NewGuid().ToString());
        }
        public string ReportName { get; set; }
        public int ReportId { get; set; }
    }
}
