using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contact.Service.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Service.Services;
using Contact.ServiceTests.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Contact.Service.Dtos;

namespace Contact.Service.Controllers.Tests
{
    [TestClass()]
    public class ContactControllerTests
    {
        private readonly ContactController _controller;
        private readonly IContactService _contactService;
        public ContactControllerTests()
        {
            _contactService = new ContactFakeService();
            _controller = new ContactController(_contactService);
        }


        [TestMethod()]
        public async Task GetAllTest()
        {
            var actionResult = await _controller.GetAll();
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<List<ContactDto>>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task GetTest()
        {
            var actionResult = await _controller.GetById("1");
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<ContactWithCommunicationsDto>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task CreateTest()
        {
            ContactCreateDto contactCreateDto = new ContactCreateDto {FirstName = "Oguzhan", LastName = "Yerlikaya", Company = "Rise Tech" };
            var actionResult = await _controller.Create(contactCreateDto);
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<ContactDto>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task UpdateTest()
        {
            ContactUpdateDto contactUpdateDto = new ContactUpdateDto { Id = "3", FirstName = "Oguzhan", LastName = "Yerlikaya", Company = "Holywood" };
            var actionResult = await _controller.Update(contactUpdateDto);
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<NoContent>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            var actionResult = await _controller.DeleteById("1");
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<NoContent>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
        }

        [TestMethod()]
        public async Task GetAllReportDataTest()
        {
            var actionResult = await _controller.GetAllReportData();
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<List<ContactWithCommunicationsDto>>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }
    }
}