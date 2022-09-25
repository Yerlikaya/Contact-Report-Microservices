using Report.Service.Models;

namespace Report.Service.Dtos
{
    public class ReportCreateDto
    {
        public DateTime CreatedDate { get; set; }
        public ReportStatusType Status { get; set; } = ReportStatusType.WAITING;
        public string ReportPath { get; set; }
    }
}
