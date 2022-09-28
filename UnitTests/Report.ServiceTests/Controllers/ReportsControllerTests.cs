using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Report.Service.Controllers;
using Report.Service.Dtos;
using Report.Service.Services;
using Report.ServiceTests.Services;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Service.Controllers.Tests
{
    [TestClass()]
    public class ReportsControllerTests
    {
        private readonly IReportService _reportService;
        private readonly IRabbitMQPublisherService _rabbitMQPublisherService;
        private readonly ReportsController _controller;

        public ReportsControllerTests()
        {
            _reportService = new ReportFakeService();
            _rabbitMQPublisherService = new RabitMQMockService();
            _controller = new ReportsController(_reportService, _rabbitMQPublisherService);
        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            var actionResult = await _controller.GetAll();
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<List<ReportDto>>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task GetByIdTest()
        {
            var actionResult = await _controller.GetById(1);
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<ReportDto>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task UpdateTest()
        {
            ReportDto report = new ReportDto { Id = 1, CreatedDate = DateTime.Now.AddHours(-1), ReportPath = "TestPath1", Status = Models.ReportStatusType.COMPLETED };
            var actionResult = await _controller.Update(report);
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<NoContent>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
        }
    }
}