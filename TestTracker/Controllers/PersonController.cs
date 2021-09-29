using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IMapper mapper;
        private readonly PeopleDbContext dbContext;

        public PersonController(IMapper mapper, PeopleDbContext peopleDbContext) {
            this.mapper = mapper;
            this.dbContext = peopleDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PersonDTO>> GetAll()
        {
            IEnumerable<PersonDTO> list = dbContext.People.AsEnumerable().Select(p => mapper.Map<PersonDTO>(p));
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PersonDTO> Get(int id)
        {
            Person person = dbContext.People.Find(id);
            if (person is null) {
                return NotFound();
            }
            return Ok(mapper.Map<PersonDTO>(person));
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ActionPersonDTO value)
        {
            if (value.address is null || value.name is null) {
                return BadRequest();
            }
            Person person = mapper.Map<Person>(value);
            dbContext.People.Add(person);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = person.id }, person);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Person person = await dbContext.People.FindAsync(id);
            if (person is null) {
                return NotFound();
            }
            dbContext.People.Remove(person);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] JsonPatchDocument<ActionPersonDTO> personPatch) {
            Person person = await dbContext.People.FindAsync(id);
            if (person is null) {
                return NotFound();
            }
            ActionPersonDTO actionPersonDTO = mapper.Map<ActionPersonDTO>(person);
            personPatch.ApplyTo(actionPersonDTO);
            mapper.Map(actionPersonDTO, person);
            await dbContext.SaveChangesAsync();
            return Ok(person);
        }
    }
}