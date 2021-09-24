using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using TestTracker.Context;
using TestTracker.Models;
using TestTracker.Models.DTO;

namespace TestTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly PeopleDbContext dbContext;

        public PersonController(PeopleDbContext peopleDbContext) {
            this.dbContext = peopleDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PersonDTO>> GetAll()
        {
            IEnumerable<PersonDTO> list = dbContext.People.AsEnumerable().Select(p => {
                return new PersonDTO() {
                    id = p.id,
                    name = p.name,
                    address = p.address
                };
            });
            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<PersonDTO> Get(int id)
        {
            Person person = dbContext.People.Find(id);
            if (person is null) {
                return NotFound();
            }
            return Ok(new PersonDTO() {
                id = person.id,
                name = person.name,
                address = person.address
            });
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ActionPersonDTO value)
        {
            if (value.address is null || value.name is null) {
                return BadRequest();
            }

            Person person = new() {
                name = value.name,
                address = value.address
            };
            dbContext.People.Add(person);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = person.id }, person);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Person person = dbContext.People.Find(id);
            if (person is null) {
                return NotFound();
            }
            dbContext.People.Remove(person);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}