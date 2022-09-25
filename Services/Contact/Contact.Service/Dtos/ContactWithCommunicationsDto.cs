namespace Contact.Service.Dtos
{
    public class ContactWithCommunicationsDto: ContactDto
    {
        public ContactWithCommunicationsDto()
        {
            Communications = new List<CommunicationDto>();
        }
        public List<CommunicationDto> Communications { get; set; }
    }
}
