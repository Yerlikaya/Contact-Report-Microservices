using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Report.Service.DBContext;
using Report.Service.Dtos;
using Report.Service.Models;
using Report.Service.Services;
using Shared.BaseController;

namespace Report.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : CustomBaseController
    {
        private readonly IReportService _reportService;
        private readonly RabbitMQPublisherService _rabbitMQPublisherService;

        public ReportsController(IReportService reportService, RabbitMQPublisherService rabbitMQPublisherService)
        {
            _reportService = reportService;
            _rabbitMQPublisherService = rabbitMQPublisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var response = await _reportService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport(int id)
        {
            var response = await _reportService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> PostReport(ReportCreateDto report)
        {
            _rabbitMQPublisherService.Publish(new CreateReportEvent { ReportName = CreateReportEvent.GetReportName(report.CreatedDate) });
            var response = await _reportService.Create(report);
            return CreateActionResultInstance(response);
        }
    }
}
