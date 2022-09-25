namespace Report.Service.Models
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public ReportStatusType Status { get; set; }
        public string ReportPath { get; set; }
    }
    public enum ReportStatusType
    {
        INPROGRESS,
        COMPLETED
    }
}
