using Report.Service.Dtos;
using Shared.Dtos;

namespace Report.Service.Services
{
    public interface IReportService
    {
        Task<Response<List<ReportDto>>> GetAllAsync();
        Task<Response<ReportDto>> GetByIdAsync(int id);
        Task<Response<ReportDto>> Create(ReportCreateDto reportDto);
        Task<Response<NoContent>> Update(ReportDto reportDto);
        Task<Response<NoContent>> Delete(int id);
    }
}
