using System;
using ProjectJ4s.DAO;
using ProjectJ4s.Models;
using System.Collections.Generic;
using ProjectJ4s.Middlewares;
using Microsoft.AspNetCore.Mvc;
using ProjectJ4s.ModelsInput;
using System.Threading.Tasks;

namespace ProjectJ4s.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonDAO _personDAO;
        public PersonController(PersonDAO personDAO) {
            _personDAO = personDAO;
        }
        [HttpPost]
        public async Task<ActionResult<PersonModelInput>> add(PersonModelInput personModelInput)
        {
            Person person = new PersonMiddleware().ValidadeDataPerson(personModelInput.Name, personModelInput.DateBirth);
           
            return CreatedAtAction(null, person);
        }
        [HttpPost]
        public async Task<ActionResult> list([FromBody] int perpage, int page)
        {
            var list = await this._personDAO.Get(perpage, page);
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet("/list/{id}")]
        public async Task<ActionResult> GetOne(string id)
        {
            var person = new PersonMiddleware().ValidadeId(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }
        /*private int GetTotalPages(int perpage)
        {
            double a = (double)_personDAO.GetTotal() / (double)perpage;
            return (int)Math.Ceiling(a);
        }
        */
    }
}