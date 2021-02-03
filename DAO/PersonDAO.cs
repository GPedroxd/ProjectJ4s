using ProjectJ4s.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using ProjectJ4s.DAO;

namespace ProjectJ4s.DAO
{
    public class PersonDAO
    {
        private readonly IMongoCollection<Person> _peaple;

        public PersonDAO(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _peaple = database.GetCollection<Person>("person");
        }

        public List<Person> Get() =>
            _peaple.Find(person => true).ToList();

        public Person Get(string id) =>
            _peaple.Find<Person>(person => person.Id == id).SingleOrDefault();

        public Person Create(Person person)
        {
            _peaple.InsertOneAsync(person);
            return person;
        }

        public void Update(string id, Person person) =>
            _peaple.ReplaceOneAsync(book => person.Id == id, person);

        public void Remove(Person person) =>
            _peaple.DeleteOneAsync(person => person.Id == person.Id);

        public void Remove(string id) =>
            _peaple.DeleteOne(person => person.Id == id);
    }
}