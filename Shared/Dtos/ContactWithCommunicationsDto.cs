namespace Shared.Dtos
{
    public class ContactWithCommunicationsDto: ContactDto
    {
        public List<CommunicationDto> Communications { get; set; }
    }
}
