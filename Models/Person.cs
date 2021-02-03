using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectJ4s.Models
{
    public class Person 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string  Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("dateBirth")]
        public DateTime DateBirth { get; set;}

        public Person(String name, DateTime dateBirth)
        {
            this.DateBirth = dateBirth;
            this.Name = name;
        }
        public Person(){
            
        }
    }
}