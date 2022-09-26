using Contact.Service.Dtos;
using Shared.Dtos;

namespace Contact.Service.Services
{
    public interface ICommunicationService
    {
        Task<Response<List<CommunicationDto>>> GetAllAsync();
        Task<Response<List<CommunicationDto>>> GetAllByContactId(string contactId);
        Task<Response<List<CommunicationDto>>> GetAllByContactIds(List<string> contactIds);
        Task<Response<CommunicationDto>> GetById(string id);
        Task<Response<CommunicationDto>> CreateAsync(CommunicationCreateDto communication);
        Task<Response<NoContent>> UpdateAsync(CommunicationUpdateDto communication);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
