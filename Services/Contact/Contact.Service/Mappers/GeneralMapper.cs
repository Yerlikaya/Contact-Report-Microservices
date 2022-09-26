using AutoMapper;
using Contact.Service.Dtos;
using Shared.Dtos;

namespace Contact.Service.Mappers
{
    public class GeneralMapper: Profile
    {
        public GeneralMapper()
        {
            CreateMap<Models.Contact, ContactDto>().ReverseMap();
            CreateMap<Models.Contact, ContactCreateDto>().ReverseMap();
            CreateMap<Models.Contact, ContactUpdateDto>().ReverseMap();
            CreateMap<Models.Contact, ContactWithCommunicationsDto>().ReverseMap();

            CreateMap<Models.Communication, CommunicationDto>().ReverseMap();
            CreateMap<Models.Communication, CommunicationCreateDto>().ReverseMap();
            CreateMap<Models.Communication, CommunicationUpdateDto>().ReverseMap();
        }
    }
}
