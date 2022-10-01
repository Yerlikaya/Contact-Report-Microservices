using Contact.Service.Dtos;
using Contact.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseController;

namespace Contact.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : CustomBaseController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _contactService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _contactService.GetById(id);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactCreateDto contact)
        {
            var response = await _contactService.CreateAsync(contact);
            return CreateActionResultInstance(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(ContactUpdateDto contact)
        {
            var response = await _contactService.UpdateAsync(contact);
            return CreateActionResultInstance(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteById(string id)
        {
            var response = await _contactService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }

        [HttpGet]
        [Route("GetContactStatistics")]
        public async Task<IActionResult> GetAllReportData()
        {
            var response = await _contactService.GetAllContactWithCommunicationsAsync();
            return CreateActionResultInstance(response);
        }
    }
}
