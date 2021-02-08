using ContactListApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsRepository repository;

        public ContactsController(IContactsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Person>))]
        public IActionResult GetAll() => Ok(repository.GetAll());
         
        [HttpGet("findByName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Person>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByNameFilter([FromQuery(Name = "nameFilter")] string nameFilter)
        {
            IEnumerable<Person> contacts;
            try
            {
                contacts = repository.GetByName(nameFilter);
            }
            catch (ArgumentException)
            {
                return BadRequest("Invalid or missing name");
            }
            return Ok(contacts);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Person))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddPerson([FromBody] Person person)
        {
            if (person.Id < 0 || string.IsNullOrWhiteSpace(person.FirstName) || string.IsNullOrWhiteSpace(person.LastName) || string.IsNullOrWhiteSpace(person.Email)) return BadRequest("Invalid input (e.g. required field missing or empty)");
            repository.Add(person);
            return Created(nameof(AddPerson), person);
        }

        [HttpDelete("{personId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int personId)
        {
            if (personId < 0) return BadRequest("Invalid ID supplied");
            try
            {
                repository.Delete(personId);
            }
            catch (ArgumentException)
            {
                return NotFound("Person not found");
            }
            return NoContent();
        }
    }
}
