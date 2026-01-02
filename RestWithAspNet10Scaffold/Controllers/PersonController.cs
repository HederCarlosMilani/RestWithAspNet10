using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Files.Exporters.Factory;
using RestWithAspNet10Scaffold.Hypermedia.Helpers;
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

    [HttpPatch("disable/{id}")]
    [ProducesResponseType(200, Type = typeof(PersonDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult Disable(long id)
    {
        _logger.LogInformation($"Disabling person with id {id}");
        if (_personServices.Disable(id) == null)
        {
            _logger.LogError($"Fail disabling person with id {id}");
            return NotFound();
        }
        _logger.LogDebug($"Success disabling person with id {id}");
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
            _logger.LogError($"Fail enabling person with id {id}");
            return NotFound();
        }

        _logger.LogDebug($"Success enabling person with id {id}");
        return Ok(_personServices.Enable(id));
    }

    [HttpGet("find-by-name")]
    [ProducesResponseType(200, Type = typeof(List<PersonDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult FindByName([FromQuery] string? firstName, [FromQuery] string? lastName)
    {
        _logger.LogInformation($"Finding persons by name: {firstName} {lastName}");
        return Ok(_personServices.FindByName(firstName, lastName));
    }
    
    [HttpGet("{sortDirection}/{pageSize}/{page}")]
    [ProducesResponseType(200, Type = typeof(PagedSearchDto<PersonDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult GetPaged([FromQuery] string? name, [FromRoute] string sortDirection, [FromRoute] int pageSize, [FromRoute] int page)
    {
        _logger.LogInformation("Getting persons paged: {Name}, {SortDirection}, {PageSize}, {Page}", name, sortDirection, pageSize, page);
        return Ok(_personServices.FindWithPagedSearch(name, sortDirection, pageSize, page));
    }
    
    [HttpPost("mass-create")]
    [ProducesResponseType(200, Type = typeof(List<PersonDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> MassCreate([FromForm] FileUploadDto fileUploadDto)
    {
        _logger.LogInformation("Mass creating persons from file: {FileName}", fileUploadDto.File?.FileName);
        var persons = await _personServices.MassCreateAsync(fileUploadDto.File);
        _logger.LogDebug($"Created {persons.Count} persons");
        return Ok(persons);
    }
    
    [HttpGet("export/{sortDirection}/{pageSize}/{page}")]
    [ProducesResponseType(200, Type = typeof(FileContentResult))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [Produces(MediaTypes.ApplicationExcel, MediaTypes.ApplicationCsv)]
    public IActionResult ExportPagedSearch([FromQuery] string? name, [FromRoute] string sortDirection, [FromRoute] int pageSize, [FromRoute] int page, [FromHeader(Name = "Accept")] string acceptHeader)
    {
        _logger.LogInformation("Exporting persons paged: {Name}, {SortDirection}, {PageSize}, {Page}, {AcceptHeader}", name, sortDirection, pageSize, page, acceptHeader);
        if (string.IsNullOrEmpty(acceptHeader)) 
        {
            _logger.LogWarning("Accept header is empty.");
            return BadRequest("Accept header is required.");
        }
        var fileResult = _personServices.ExportPagedSearch(name, sortDirection, pageSize, page, acceptHeader);

        try
        {
            _logger.LogDebug("Exported persons to file with content type: {ContentType}", fileResult.ContentType);
            return fileResult;
        } 
        catch (NotSupportedException ex)
        {
            _logger.LogError(ex, "Accept header format not supported: {AcceptHeader}", acceptHeader);
            return StatusCode(StatusCodes.Status415UnsupportedMediaType, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exporting persons to file.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while exporting the file.");
        }
    }
}