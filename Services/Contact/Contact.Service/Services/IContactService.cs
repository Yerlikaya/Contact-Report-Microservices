using Contact.Service.Dtos;
using Shared.Dtos;

namespace Contact.Service.Services
{
    public interface IContactService
    {
        Task<Response<List<ContactDto>>> GetAllAsync();
        Task<Response<ContactWithCommunicationsDto>> GetById(string id);
        Task<Response<List<ContactWithCommunicationsDto>>> GetAllContactWithCommunicationsAsync();
        Task<Response<ContactDto>> CreateAsync(ContactCreateDto contact);
        Task<Response<NoContent>> UpdateAsync(ContactUpdateDto contact);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
