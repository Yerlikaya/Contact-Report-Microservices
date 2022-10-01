using Report.Service.Dtos;
using Shared.Dtos;

namespace Report.Service.Services
{
    public interface IReportService
    {
        Task<Response<List<ReportDto>>> GetAllAsync();
        Task<Response<ReportDto>> GetByIdAsync(int id);
        Task<Response<ReportDto>> CreateAsync();
        Task<Response<NoContent>> UpdateAsync(ReportDto reportDto);
        Task<Response<NoContent>> DeleteAsync(int id);
    }
}
