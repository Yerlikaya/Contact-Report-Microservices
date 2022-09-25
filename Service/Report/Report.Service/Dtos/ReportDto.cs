using Report.Service.Models;

namespace Report.Service.Dtos
{
    public class ReportDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public ReportStatusType Status { get; set; }
        public string ReportPath { get; set; }
    }
}
