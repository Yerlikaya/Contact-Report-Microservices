﻿using MongoDB.Bson.Serialization.Attributes;
using Shared.Dtos;

namespace Contact.Service.Models
{
    public class Communication
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        
        public CommunicationType CommunicationType { get; set; }
        
        public string Address { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ContactId { get; set; }
    }
}
