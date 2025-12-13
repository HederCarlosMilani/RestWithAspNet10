using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Service;

namespace RestWithAspNet10Scaffold.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : Controller
{
    private readonly IPersonServices _personServices;
    public PersonController(IPersonServices personServices)
    {
        _personServices = personServices;
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        return Ok(_personServices.FindById(id));
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_personServices.FindAll());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Model.Person person)
    {
        return Ok(_personServices.Create(person));
    }

    [HttpPut]
    public IActionResult Put([FromBody] Model.Person person)
    {
        return Ok(_personServices.Update(person));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        _personServices.Delete(id);
        return NoContent();
    }
}