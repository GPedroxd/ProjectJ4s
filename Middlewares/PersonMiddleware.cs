using System;
using MongoDB.Bson;
using ProjectJ4s.Models;
using ProjectJ4s.DAO;
using System.Globalization;

namespace ProjectJ4s.Middlewares
{
    public class PersonMiddleware
    {
        PersonDAO personDAO;

        public PersonMiddleware(PersonDAO personDAO)
        {
            this.personDAO = personDAO;
        }
        public Person ValidadeDataPerson(string name, string dateBirth)
        {
            Person person = new Person();

            if(name.Length < 2)
            {
                return null;
            }
            person.Name = name.Trim();
            dateBirth = dateBirth.Trim();
            var dt = ValidateData(dateBirth);
            if(dt == default)
            {
                return null;
            }
            person.dateBirth = dt;
            return person;
        }
        public Person ValidadeDataPerson(string name, string dateBirth, Person person)
        {

            if (name.Length < 2)
            {
                return null;
            }
            person.Name = name;
            var dt = ValidateData(dateBirth);
            if (dt == default)
            {
                return null;
            }
            person.dateBirth = dt;
            return person;
        }

        public Person ValidateId(string id)
        {   
            if (!ObjectId.TryParse(id, out _))
            {
                return null;
            }
            return this.personDAO.GetOne(id);
        }

        public DateTime ValidateData(string date)
        {
            string format = "dd/MM/yyyy";
            if(!DateTime.TryParseExact(date, format, new CultureInfo("pt-BR"), DateTimeStyles.None, out _))
            {
                return default;
            }
            DateTime dt = DateTime.Parse(date);
            if(dt > DateTime.Now)
            {
                return default;
            }
            return dt;
        }
    }
}
