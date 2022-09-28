using AutoMapper;
using Contact.Service.Dtos;
using Contact.Service.Mappers;
using Contact.Service.Services;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactModel = Contact.Service.Models.Contact;

namespace Contact.ServiceTests.Services
{
    public class ContactFakeService : IContactService
    {
        private readonly List<ContactModel> contextDb;
        private readonly IMapper _mapper;
        public ContactFakeService()
        {
            contextDb = new List<ContactModel>() {new ContactModel {Id ="1",FirstName ="Ali", LastName="Can", Company="Rise Tech"},
            new ContactModel {Id ="2",FirstName ="Chen", LastName="Woo", Company="Aliexpress"},
            new ContactModel {Id ="3",FirstName ="Jonny", LastName="Deph", Company="Holywood"}};

            var config = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapper>());
            _mapper = config.CreateMapper();
        }
        public async Task<Response<ContactDto>> CreateAsync(ContactCreateDto contact)
        {
            var newContact = _mapper.Map<ContactModel>(contact);
            contextDb.Add(newContact);
            return Response<ContactDto>.Success(_mapper.Map<ContactDto>(newContact), 200);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var contact = contextDb.FirstOrDefault(c => c.Id == id);
            var result = contextDb.Remove(contact);
            if (!result)
            {
                return Response<NoContent>.Fail("Communications not found!", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<List<ContactDto>>> GetAllAsync()
        {
            var contacts = contextDb;
            return Response<List<ContactDto>>.Success(_mapper.Map<List<ContactDto>>(contacts), 200);
        }

        public async Task<Response<List<ContactStatisticsDto>>> GetAllContactWithCommunicationsAsync()
        {
            List<ContactStatisticsDto> contactStatistics = new List<ContactStatisticsDto>();
            var contactIds = contextDb.Select(x => x.Id).ToList();
            CommunicationFakeService communicationFakeService = new CommunicationFakeService();
            var communications = (await communicationFakeService.GetAllByContactIdsAsync(contactIds)).Data;

            var locationGroup = communications.Where(x => x.CommunicationType == CommunicationType.LOCATION)
                .GroupBy(x => x.Address).Select(x => x.Key).ToList();

            locationGroup.ForEach(location =>
            {
                var contactIdsOfLocation = communications.Where(x => x.Address == location && x.CommunicationType == CommunicationType.LOCATION)
                .Select(x => x.ContactId).Distinct().ToList();//Same contact filtered.

                var phoneCountOfLocation = communications.Where(x => contactIdsOfLocation.Contains(x.ContactId) && x.CommunicationType == CommunicationType.PHONE)
                .Select(x => x.Address).Distinct().Count();//Same phone filtered.

                contactStatistics.Add(new ContactStatisticsDto
                {
                    Location = location,
                    ContactCount = contactIdsOfLocation.Count,
                    PhoneCount = phoneCountOfLocation
                });
            });

            return Response<List<ContactStatisticsDto>>.Success(contactStatistics, 200);
        }

        public async Task<Response<ContactWithCommunicationsDto>> GetById(string id)
        {
            var contact = contextDb.FirstOrDefault(x => x.Id == id);
            if (contact == null)
            {
                return Response<ContactWithCommunicationsDto>.Fail("Contact not found!", 404);
            }
            var contactDto = _mapper.Map<ContactWithCommunicationsDto>(contact);
            CommunicationFakeService communicationFakeService = new CommunicationFakeService();
            var communicationsResponse = await communicationFakeService.GetAllByContactIdAsync(contact.Id);
            contactDto.Communications = communicationsResponse.Data;
            return Response<ContactWithCommunicationsDto>.Success(contactDto, 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(ContactUpdateDto contact)
        {
            var updateContact = _mapper.Map<ContactModel>(contact);
            var contactOldRecord = contextDb.FirstOrDefault(x => x.Id == updateContact.Id);

            if (contactOldRecord == null)
            {
                return Response<NoContent>.Fail("Contact not found!", 404);
            }

            contextDb.Remove(contactOldRecord);
            contextDb.Add(updateContact);
            return Response<NoContent>.Success(204);
        }
    }
}
