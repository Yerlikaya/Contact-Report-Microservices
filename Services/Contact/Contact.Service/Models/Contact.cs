using MongoDB.Bson.Serialization.Attributes;

namespace Contact.Service.Models
{
    public class Contact
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Company { get; set; }
        
        [BsonIgnore]
        public List<Communication> Communications { get; set; }
    }
}
