using AutoMapper;
using Report.Service.Dtos;
using Report.Service.Mappers;
using Report.Service.Services;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportModel = Report.Service.Models.Report;

namespace Report.ServiceTests.Services
{
    public class ReportFakeService : IReportService
    {
        private readonly List<ReportModel> contextDb;
        private readonly IMapper _mapper;

        public ReportFakeService()
        {
            contextDb = new List<ReportModel>() { new ReportModel() { Id = 1, ReportPath = "Parth1", CreatedDate = DateTime.Now, Status = Service.Models.ReportStatusType.WAITING },
            new ReportModel() { Id = 2, ReportPath = "Parth2", CreatedDate = DateTime.Now.AddDays(-1), Status = Service.Models.ReportStatusType.INPROGRESS },
            new ReportModel() { Id = 3, ReportPath = "Parth3", CreatedDate = DateTime.Now.AddDays(-2), Status = Service.Models.ReportStatusType.COMPLETED },
            new ReportModel() { Id = 4, ReportPath = "Parth4", CreatedDate = DateTime.Now.AddDays(-3), Status = Service.Models.ReportStatusType.FAILED },};

            var config = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapper>());
            _mapper = config.CreateMapper();
        }

        public async Task<Response<ReportDto>> CreateAsync()
        {
            ReportModel report = new ReportModel { Status = Service.Models.ReportStatusType.WAITING, CreatedDate = DateTime.Now.ToUniversalTime(), ReportPath = "NoPath" };
            contextDb.Add(report);
            return Response<ReportDto>.Success(_mapper.Map<ReportDto>(report), 200);
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            var report = contextDb.FirstOrDefault(c => c.Id == id);
            var result = contextDb.Remove(report);
            if (!result)
            {
                return Response<NoContent>.Fail("Report not found!", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<List<ReportDto>>> GetAllAsync()
        {
            var report = contextDb;
            return Response<List<ReportDto>>.Success(_mapper.Map<List<ReportDto>>(report), 200);
        }

        public async Task<Response<ReportDto>> GetByIdAsync(int id)
        {
            var report = contextDb.FirstOrDefault(x => x.Id == id);
            if (report == null)
            {
                return Response<ReportDto>.Fail("Report not found!", 404);
            }
            return Response<ReportDto>.Success(_mapper.Map<ReportDto>(report), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(ReportDto reportDto)
        {
            var updateReport = _mapper.Map<ReportModel>(reportDto);
            var oldReport = contextDb.FirstOrDefault(x => x.Id == updateReport.Id);

            if (oldReport == null)
            {
                return Response<NoContent>.Fail("Report not found!", 404);
            }

            contextDb.Remove(oldReport);
            contextDb.Add(updateReport);
            return Response<NoContent>.Success(204);
        }
    }
}
