using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectJ4s.Models;
using ProjectJ4s.DAO;
using ProjectJ4s.Middlewares;
using System.Collections.ObjectModel;

namespace ProjectJ4s.Controllers
{
    [Route("api/[Controller]/[Action]")]
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
        public async Task<ActionResult> CreateOrUpdate(string name, string dateBirth, string id = "")
        {
            Person person;
            if (id.Equals(""))
            {
                person = this.PersonMiddleware.ValidadeDataPerson(name, dateBirth);
                if (person == default)
                {
                    return StatusCode(500, "Wrong Data!");
                }
                person = this.PersonDAO.add(person);
                if (person == default)
                {
                    return StatusCode(500, "Database error conection!");
                }
                return Ok();
            }
            person = this.PersonMiddleware.ValidateId(id);
            if (person == default)
            {
                return StatusCode(500, "Invalid Id!");
            }
            person = this.PersonMiddleware.ValidadeDataPerson(dateBirth: dateBirth, person: person, name: name);
            if (person == default)
            {
                return StatusCode(500, "Person not found!");
            }
            person = this.PersonDAO.edit(person);
            if (person == default)
            {
                return StatusCode(500, "Database error conection!");
            }
            return Ok();
        }
        
        [HttpGet("{id}")]        
        public async Task<ActionResult> GetOne(string id)
        {
            var person = PersonMiddleware.ValidateId(id);
            if(person == default)
            {
                return StatusCode(500, "Person not found!");
            }
            return Ok(person);
        }

        [HttpPost()]

        public async Task<ActionResult> List(int pageSize, int currentPage)
        {
            if(pageSize <= 0)
            {
                return ???
            }

            var paging = new PagingMiddleware().List(pageSize, currentPage);
            if (paging == default)
            {
                return Ok("Nothing register found");
            }

            return Ok(paging);
        }
        //public string delete(string id)
        //{
            
        //}
    }
}