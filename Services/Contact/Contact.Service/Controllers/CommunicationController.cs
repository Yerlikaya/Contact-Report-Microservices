using Contact.Service.Dtos;
using Contact.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseController;

namespace Contact.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationController : CustomBaseController
    {
        private readonly ICommunicationService _communicationService;

        public CommunicationController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _communicationService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string userId)
        {
            var response = await _communicationService.GetById(userId);
            return CreateActionResultInstance(response);
        }
        [HttpGet]
        [Route("GetAllByContactId/{contactId}")]
        public async Task<IActionResult>GetAllByContactId(string contactId)
        {
            var response = await _communicationService.GetAllByContactId(contactId);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommunicationCreateDto communication)
        {
            var response = await _communicationService.CreateAsync(communication);
            return CreateActionResultInstance(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CommunicationUpdateDto communication)
        {
            var response = await _communicationService.UpdateAsync(communication);
            return CreateActionResultInstance(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteById(string id)
        {
            var response = await _communicationService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}
