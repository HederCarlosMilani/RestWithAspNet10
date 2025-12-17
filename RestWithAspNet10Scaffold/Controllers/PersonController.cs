using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Services;

namespace RestWithAspNet10Scaffold.Controllers;

[ApiController]
[Route("[controller]")]
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
    public IActionResult Get(long id)
    {
        _logger.LogInformation($"Getting person with id {id}");
        return Ok(_personServices.FindById(id));
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation($"Getting all persons");
        return Ok(_personServices.FindAll());
    }

    [HttpPost]
    public IActionResult Post([FromBody] PersonDto person)
    {
        _logger.LogInformation($"Adding new person {person.FirstName} {person.LastName}");
        return Ok(_personServices.Create(person));
    }

    [HttpPut]
    public IActionResult Put([FromBody] PersonDto person)
    {
        _logger.LogInformation($"Updating person with id {person.Id}");
        return Ok(_personServices.Update(person));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        _logger.LogInformation($"Deleting person with id {id}");
        _personServices.Delete(id);
        return NoContent();
    }
}