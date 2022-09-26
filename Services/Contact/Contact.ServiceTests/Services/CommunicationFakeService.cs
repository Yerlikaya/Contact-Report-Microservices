using Contact.Service.Dtos;
using Contact.Service.Models;
using Contact.Service.Services;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.ServiceTests.Services
{
    public class CommunicationFakeService : ICommunicationService
    {
        private readonly List<Communication> contextDb;
        public CommunicationFakeService()
        {
            contextDb = new List<Communication>() {new Communication {Id ="1",CommunicationType = CommunicationType.EMAIL, Address ="sample@email.com", ContactId="11"},
            new Communication {Id ="2",CommunicationType = CommunicationType.PHONE, Address ="909998887766", ContactId="22"},
            new Communication {Id ="3",CommunicationType = CommunicationType.LOCATION, Address ="ANTALYA", ContactId="22"}};
        }
        public Task<Response<CommunicationDto>> CreateAsync(CommunicationCreateDto communication)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<CommunicationDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<CommunicationDto>>> GetAllByContactId(string contactId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<CommunicationDto>>> GetAllByContactIds(List<string> contactIds)
        {
            throw new NotImplementedException();
        }

        public Task<Response<CommunicationDto>> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> UpdateAsync(CommunicationUpdateDto communication)
        {
            throw new NotImplementedException();
        }
    }
}
