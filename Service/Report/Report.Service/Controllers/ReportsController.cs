using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Report.Service.Context;
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

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
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
            var response = await _reportService.Create(report);
            return CreateActionResultInstance(response);
        }
    }
}
