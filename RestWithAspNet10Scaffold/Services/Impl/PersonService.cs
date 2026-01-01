using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Mapster;
using RestWithAspNet10Scaffold.Data.Convert.Impl;
using RestWithAspNet10Scaffold.Data.Dto;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Files.Importers.Factory;
using RestWithAspNet10Scaffold.Hypermedia.Helpers;
using RestWithAspNet10Scaffold.Models;
using RestWithAspNet10Scaffold.Repositories;

namespace RestWithAspNet10Scaffold.Services.Impl;

public class PersonService(IPersonRepository personRepository, FileImporterFactory fileImporterFactory, ILogger<PersonService> logger)
    : IPersonServices
{
    private readonly PersonConverter _personConverter = new();

    public PersonDto Create(PersonDto personDto)
    {
        Person person = _personConverter.Parser(personDto);
        return  _personConverter.Parser(personRepository.Create(person));
    }

    public PersonDto? FindById(long id)
    {
        return _personConverter.Parser(personRepository.FindById(id));
    }

    public List<PersonDto> FindAll()
    {
        return _personConverter.ParserList(personRepository.FindAll());
    }

    public PersonDto? Update(PersonDto personDto)
    {
        Person person = _personConverter.Parser(personDto);
        return  _personConverter.Parser(personRepository.Update(person));
    }

    public void Delete(long id)
    {
        var person = personRepository.FindById(id);
        if (person == null) return;
        personRepository.Delete(person);
    }
    
    public PersonDto? Disable(long id)
    {
        var person = personRepository.Disable(id);
        return person.Adapt<PersonDto>();
    }
    
    public PersonDto? Enable(long id)
    {
        var person = personRepository.Enable(id);
        return person.Adapt<PersonDto>();
    }

    public List<PersonDto> FindByName(string? firstName, string? lastName)
    {
        var persons = personRepository.FindByName(firstName, lastName);
        return _personConverter.ParserList(persons);
    }

    public PagedSearchDto<PersonDto> FindWithPagedSearch(string? name, string sortDirection, int pageSize, int page)
    {
        var pagedSearch = personRepository.FindWithPagedSearch(name, sortDirection, pageSize, page);
        
        return pagedSearch.Adapt<PagedSearchDto<PersonDto>>();
    }

    public Task<List<PersonDto>> MassCreateAsync(IFormFile? file)
    {
        if (file == null || file.Length == 0)
        {
            logger.LogWarning("No file provided for mass creation.");
            return Task.FromResult(new List<PersonDto>());
        }
        
        using var fileStream = file.OpenReadStream();
        var fileName = file.FileName;

        try
        {
            var importer = fileImporterFactory.GetImporter(fileName);
            var personsDto = importer.ImportFileAsync(fileStream);
            
            var entities = personsDto.Result.Select(dto => personRepository.Create(
                dto.Adapt<Person>()
                )).ToList();
            return Task.FromResult(entities.Adapt<List<PersonDto>>());
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            throw;
        }
    }
}