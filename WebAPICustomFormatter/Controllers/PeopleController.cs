using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPICustomFormatter.DTOs;
using WebAPICustomFormatter.Entities;
using WebAPICustomFormatter.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPICustomFormatter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PeopleController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }


        // GET: api/<PeopleController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var people = await _personService.GetAllAsync();
            if (people == null)
                return NotFound();
            var peopleDTOs = _mapper.Map<IEnumerable<PersonDTO>>(people);
            return Ok(peopleDTOs);
        }

        // GET api/<PeopleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _personService.GetAsync(p => p.Id == id);
            if (item == null)
                return NotFound();
            var itemDTO = _mapper.Map<PersonDTO>(item);
            return Ok(itemDTO);
        }

        // POST api/<PeopleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var person = _mapper.Map<Person>(value);
            await _personService.AddAsync(person);
            var createdPersonDto = _mapper.Map<PersonDTO>(person);

            return CreatedAtAction(nameof(Get), new { id = createdPersonDto.Id }, createdPersonDto);
        }

        // PUT api/<PeopleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PersonDTO personDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingPerson = await _personService.GetAsync(p => p.Id == id);
            if (existingPerson == null)
                return NotFound();

            _mapper.Map(personDto, existingPerson);
            await _personService.UpdateAsync(existingPerson);

            return NoContent();
        }

        // DELETE api/<PeopleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _personService.GetAsync(p => p.Id == id);
            if (person == null)
                return NotFound();
            await _personService.DeleteAsync(person);

            return NoContent();
        }

    }
}
