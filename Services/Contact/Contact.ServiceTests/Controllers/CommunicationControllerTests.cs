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

namespace Contact.Service.Controllers.Tests
{
    [TestClass()]
    public class CommunicationControllerTests
    {

        [TestMethod()]
        public async Task GetTestAsync()
        {
            CommunicationFakeService fakeService = new CommunicationFakeService();
            CommunicationController controller = new CommunicationController(fakeService);
            var actionResult = await controller.GetAll();
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<List<CommunicationDto>>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task GetByIdTest()
        {
            CommunicationFakeService fakeService = new CommunicationFakeService();
            CommunicationController controller = new CommunicationController(fakeService);
            var actionResult = await controller.GetById("1");
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<CommunicationDto>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task GetAllByContactIdTest()
        {
            CommunicationFakeService fakeService = new CommunicationFakeService();
            CommunicationController controller = new CommunicationController(fakeService);
            var actionResult = await controller.GetAllByContactId("11");
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<List<CommunicationDto>>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task PostTest()
        {
            CommunicationFakeService fakeService = new CommunicationFakeService();
            CommunicationController controller = new CommunicationController(fakeService);
            CommunicationCreateDto communicationCreateDto = new CommunicationCreateDto { ContactId = "111", CommunicationType=CommunicationType.LOCATION, Address ="TURKEY" };
            var actionResult = await controller.Post(communicationCreateDto);
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<CommunicationDto>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task PutTest()
        {
            CommunicationFakeService fakeService = new CommunicationFakeService();
            CommunicationController controller = new CommunicationController(fakeService);
            CommunicationUpdateDto communicationUpdateDto = new CommunicationUpdateDto { Id="1", ContactId = "1256", CommunicationType = CommunicationType.LOCATION, Address = "TURKEY" };
            var actionResult = await controller.Put(communicationUpdateDto);
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<NoContent>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
        }

        [TestMethod()]
        public async Task DeleteByIdTest()
        {
            CommunicationFakeService fakeService = new CommunicationFakeService();
            CommunicationController controller = new CommunicationController(fakeService);
            var actionResult = await controller.DeleteById("1");
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<NoContent>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
        }
    }
}