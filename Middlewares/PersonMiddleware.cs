using System;
using MongoDB.Bson;
using ProjectJ4s.ModelsInput;
using ProjectJ4s.Models;
using ProjectJ4s.DAO;
using System.Globalization;

namespace ProjectJ4s.Middlewares
{
    public class PersonMiddleware
    {
        PersonDAO personDAO;

        public PersonMiddleware()
        {
        }
        public Person ValidadeDataPerson(string name, string dateBirth)
        {
            Person person = new Person();

            if(name.Length < 2)
            {
                return null;
            }
            person.Name = name; string formart = "dd/MM/yyyy";
            if (!DateTime.TryParseExact(dateBirth, formart, new CultureInfo("pt-br"),
                                                DateTimeStyles.None, out _))
            {
                return null;
            }
            person.DateBirth =  DateTime.Parse(dateBirth);

            return person;
        }
        public Person ValidadeDataPerson(string name, string dateBirth, Person person)
        {

            if (name.Length < 2)
            {
                return null;
            }
            person.Name = name;
            string formart = "dd/MM/yyyy";
            if (!DateTime.TryParseExact(dateBirth, formart, new CultureInfo("pt-br"),
                                                DateTimeStyles.None, out _))
            {
                return null;
            }
            person.DateBirth = DateTime.Parse(dateBirth);

            return person;
        }

        public Person ValidadeId(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return null;
            }
            return this.personDAO.GetOne(id);
        }
    }
}
