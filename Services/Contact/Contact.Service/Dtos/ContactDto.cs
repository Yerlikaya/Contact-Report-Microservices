namespace Contact.Service.Dtos
{
    public class ContactDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public List<CommunicationDto> Communications { get; set; }
    }
}
