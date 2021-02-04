using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectJ4s.Models;
using ProjectJ4s.DAO;
using ProjectJ4s.Middlewares;

namespace ProjectJ4s.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PersonController : ControllerBase

    {
        PersonDAO PersonDAO { get; }
        PersonMiddleware PersonMiddleware { get; }


        public PersonController()
        {
            PersonDAO = new PersonDAO();
            PersonMiddleware = new PersonMiddleware();

        }

        [HttpPost]
        public async Task<ActionResult> add (string name, string dateBirth)
        {
            var person = this.PersonMiddleware.ValidadeDataPerson(name, dateBirth);
            if(person == default)
            {
                return StatusCode(500); 
            }
            person = this.PersonDAO.add(person);
            if(person == default)
            {
                return StatusCode(500);
            }
            return Ok();
        }
        //public List<Person> list (params int[] param)
        //{
            
        //}
        //public Person GetOne(string id)
        //{
            
        //}
        //public int GetTotalPages(int perpage){
            
        //}
        //public string edit(string name, string dateBirth, Person person)
        //{
            
        //}
        //public string delete(string id)
        //{
            
        //}
    }
}