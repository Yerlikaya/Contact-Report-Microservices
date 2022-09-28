namespace Report.Service.Models
{
    public class RiseTechServices
    {
        
        public ServiceInfo ContactService { get; set; }
        public ServiceInfo ReportService { get; set; }
    }

    public class ServiceInfo
    {
        public string Domain { get; set; }
    }

}
