using System.Collections.Generic;
using ApiPerson.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ApiPerson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PersonService _personService;
        public PeopleController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("TeamMembers")]
        public ActionResult<List<Person>> GetTeamMembers(string teamName) =>
            _personService.GetTeamMembers(teamName);

        [HttpGet("People No Team")]
        public ActionResult<List<Person>> GetPeopleNoTeam() =>
            _personService.GetPeopleNoTeam();


        [HttpGet]
        public ActionResult<List<Person>> Get() =>
            _personService.Get();

        [HttpGet("Get Person for name")]
        public ActionResult<Person> GetName(string name)
        {
            var person = _personService.GetName(name);
            if (person == null)
            {
                return NotFound("No avaible person with that name found");
            }

            return person;
        }
        [HttpGet("{id:length(24)}", Name = "GetPerson")]
        public ActionResult<Person> Get(string id)
        {
            var person = _personService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        [HttpPost]


        public ActionResult<Person> Create(Person person)
        {
            _personService.Create(person);
            return CreatedAtRoute("GetPerson", new { id = person.Id.ToString() }, person);

        }


        [HttpPut("{id:length(24)}")]


        public IActionResult Update(string id, Person personIn)
        {
            var person = _personService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            _personService.Update(id, personIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]


        public IActionResult Delete(string id)
        {
            var person = _personService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            _personService.Remove(person.Id);

            return NoContent();
        }
    }
}
