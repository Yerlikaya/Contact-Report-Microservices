using AutoMapper;
using Contact.Service.Dtos;
using Contact.Service.Mappers;
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
        private readonly IMapper _mapper;
        public CommunicationFakeService()
        {
            contextDb = new List<Communication>() {new Communication {Id ="1",CommunicationType = CommunicationType.EMAIL, Address ="sample@email.com", ContactId="11"},
            new Communication {Id ="2",CommunicationType = CommunicationType.PHONE, Address ="909998887766", ContactId="22"},
            new Communication {Id ="3",CommunicationType = CommunicationType.LOCATION, Address ="ANTALYA", ContactId="22"}};

            var config = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapper>());
            _mapper = config.CreateMapper();
        }
        public async Task<Response<CommunicationDto>> CreateAsync(CommunicationCreateDto communication)
        {
            var newCommunication = _mapper.Map<Communication>(communication);
            contextDb.Add(newCommunication);
            return Response<CommunicationDto>.Success(_mapper.Map<CommunicationDto>(newCommunication), 200);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var communication = contextDb.FirstOrDefault(c => c.Id == id);
            var result =  contextDb.Remove(communication);
            if (!result)
            {
                return Response<NoContent>.Fail("Communications not found!", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<List<CommunicationDto>>> GetAllAsync()
        {
            var communications =  contextDb;
            return Response<List<CommunicationDto>>.Success(_mapper.Map<List<CommunicationDto>>(communications), 200);
        }

        public async Task<Response<List<CommunicationDto>>> GetAllByContactId(string contactId)
        {
            var communications = contextDb.Where(x => x.ContactId == contactId).ToList();
            if (communications.Any())
            {
                return Response<List<CommunicationDto>>.Success(_mapper.Map<List<CommunicationDto>>(communications), 200);
            }
            return Response<List<CommunicationDto>>.Fail("Communications not found!", 404);
        }

        public async Task<Response<List<CommunicationDto>>> GetAllByContactIds(List<string> contactIds)
        {
            var communications = contextDb.Where(x =>contactIds.Contains(x.ContactId)).ToList();
            if (communications.Any())
            {
                return Response<List<CommunicationDto>>.Success(_mapper.Map<List<CommunicationDto>>(communications), 200);
            }
            return Response<List<CommunicationDto>>.Fail("Communications not found!", 404);
        }

        public async Task<Response<CommunicationDto>> GetById(string id)
        {
            var communication =  contextDb.FirstOrDefault(x => x.Id == id);
            if (communication == null)
            {
                return Response<CommunicationDto>.Fail("Communication not found!", 404);
            }
            return Response<CommunicationDto>.Success(_mapper.Map<CommunicationDto>(communication), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CommunicationUpdateDto communication)
        {
            var updateCoummunication = _mapper.Map<Communication>(communication);
            var communicationOld = contextDb.FirstOrDefault(x => x.Id == updateCoummunication.Id);
            
            if (communicationOld == null)
            {
                return Response<NoContent>.Fail("Communications not found!", 404);
            }

            contextDb.Remove(communicationOld);
            contextDb.Add(updateCoummunication);
            return Response<NoContent>.Success(204);
        }
    }
}
