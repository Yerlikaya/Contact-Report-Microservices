namespace Report.Service.Models
{
    public class RiseTechServices
    {
        public string Domain { get; set; }
        public ContactServiceInfo ContactService { get; set; }
        public ReportServiceInfo ReportService { get; set; }
    }

    public class ContactServiceInfo
    {
        public int Port { get; set; }
        public string ContactReportDataGetPath { get; set; }
    }

    public class ReportServiceInfo
    {
        public int Port { get; set; }
        public string ReportUpdatePath { get; set; }
    }

}
