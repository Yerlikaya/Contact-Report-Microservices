using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contact.Service.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.ServiceTests.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Contact.Service.Dtos;
using Contact.Service.Services;

namespace Contact.Service.Controllers.Tests
{
    [TestClass()]
    public class CommunicationControllerTests
    {
        private readonly CommunicationController _controller;
        private readonly ICommunicationService _service;
        public CommunicationControllerTests()
        {
            _service = new CommunicationFakeService();
            _controller = new CommunicationController(_service);
        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            var actionResult = await _controller.GetAll();
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<List<CommunicationDto>>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task GetByIdTest()
        {
            var actionResult = await _controller.GetById("1");
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<CommunicationDto>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task GetAllByContactIdTest()
        {
            var actionResult = await _controller.GetAllByContactId("1");
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<List<CommunicationDto>>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task PostTest()
        {
            CommunicationCreateDto communicationCreateDto = new CommunicationCreateDto { ContactId = "111", CommunicationType=CommunicationType.LOCATION, Address ="TURKEY" };
            var actionResult = await _controller.Create(communicationCreateDto);
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<CommunicationDto>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task PutTest()
        {
            CommunicationUpdateDto communicationUpdateDto = new CommunicationUpdateDto { Id="1", ContactId = "1256", CommunicationType = CommunicationType.LOCATION, Address = "TURKEY" };
            var actionResult = await _controller.Update(communicationUpdateDto);
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<NoContent>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
        }

        [TestMethod()]
        public async Task DeleteByIdTest()
        {
            var actionResult = await _controller.DeleteById("1");
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<NoContent>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
        }
    }
}