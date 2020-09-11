using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakGrocery.API.Entities
{
    public class EntityBase
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public string CreatedBy { get; set; }
        [BsonElement]
        [BsonDateTimeOptions(DateOnly =false, Kind =DateTimeKind.Local, Representation = MongoDB.Bson.BsonType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
        [BsonElement]
        public string ModifiedBy { get; set; }
        [BsonElement]
        public string CreatedIP { get; set; }
        [BsonElement]
        public string ModifiedIP { get; set; }

    }
}
