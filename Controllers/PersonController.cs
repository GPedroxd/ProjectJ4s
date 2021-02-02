using System;
using ProjectJ4s.DAO;
using ProjectJ4s.Models;
using System.Collections.Generic;

namespace ProjectJ4s.Controllers
{
    public class PersonController 
    {
        PersonDAO personDAO { get; set; }
        public PersonController(){
            this.personDAO = new PersonDAO();
        }
        public void  add (string name, string dateBirth)
        {
            string [] dateFormat = dateBirth.Split('/');
            DateTime dateFinal;
            foreach(string a in dateFormat)
            {
                Console.WriteLine(a);
            }
            try
            {
                dateFinal = new DateTime(day: Convert.ToInt32(dateFormat[0]), 
                                                month: Convert.ToInt32(dateFormat[1]),
                                                 year: Convert.ToInt32(dateFormat[2]));
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            } 
            personDAO.add(new Person(name, dateFinal));     
        }
        public List<Person> list (params int[] param)
        {
            if(param[0] == 0){
                param[0] = 10;
            }
            param[1] = param[0] * (param[1] - 1);
            return personDAO.listall(param);
        }
        public int GetTotalPages(int perpage){
            decimal a = personDAO.GetTotal() / perpage;
            return (int) Math.Ceiling(a);
        }
    }
}