using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Services;

namespace RestWithAspNet10Scaffold.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("LocalPolicy")]
public class PersonController : Controller
{
    private readonly IPersonServices _personServices;
    private  readonly ILogger<PersonController> _logger;
    
    public PersonController(IPersonServices personServices, ILogger<PersonController> logger)
    {
        _personServices = personServices;
        _logger = logger;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(PersonDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult Get(long id)
    {
        _logger.LogInformation($"Getting person with id {id}");
        return Ok(_personServices.FindById(id));
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<PersonDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult Get()
    {
        _logger.LogInformation($"Getting all persons");
        return Ok(_personServices.FindAll());
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(PersonDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult Post([FromBody] PersonDto person)
    {
        _logger.LogInformation($"Adding new person {person.FirstName} {person.LastName}");
        return Ok(_personServices.Create(person));
    }

    [HttpPut]
    [ProducesResponseType(200, Type = typeof(PersonDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult Put([FromBody] PersonDto person)
    {
        _logger.LogInformation($"Updating person with id {person.Id}");
        return Ok(_personServices.Update(person));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult Delete(long id)
    {
        _logger.LogInformation($"Deleting person with id {id}");
        _personServices.Delete(id);
        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(200, Type = typeof(PersonDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult Disable(long id)
    {
        _logger.LogInformation($"Disabling person with id {id}");
        if (_personServices.Disable(id) == null)
        {
            _logger.LogError($"Disabling person with id {id}");
            return NotFound();
        }
        _logger.LogDebug($"Disabling person with id {id}");
        return Ok(_personServices.Disable(id));
    }

    [HttpPatch("enable/{id}")]
    [ProducesResponseType(200, Type = typeof(PersonDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult Enable(long id)
    {
        _logger.LogInformation($"Enabling person with id {id}");
        if (_personServices.Enable(id) == null)
        {
            _logger.LogError($"Enabling person with id {id}");
            return NotFound();
        }

        _logger.LogDebug($"Enabling person with id {id}");
        return Ok(_personServices.Enable(id));
    }
}