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
        private readonly IRabbitMQPublisherService _rabbitMQPublisherService;

        public ReportsController(IReportService reportService, IRabbitMQPublisherService rabbitMQPublisherService)
        {
            _reportService = reportService;
            _rabbitMQPublisherService = rabbitMQPublisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _reportService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _reportService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ReportCreateDto report)
        {
            var response = await _reportService.CreateAsync(report);
            _rabbitMQPublisherService.Publish(new CreateReportEvent { ReportId = response.Data.Id }, 
                Constant.ReportQueue, Constant.ReportRouting, Constant.ReportExchange);
            return CreateActionResultInstance(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(ReportDto report)
        {
            var response = await _reportService.UpdateAsync(report);
            return CreateActionResultInstance(response);
        }
    }
}
