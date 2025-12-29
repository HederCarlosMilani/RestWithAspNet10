using Mapster;
using RestWithAspNet10Scaffold.Data.Convert.Impl;
using RestWithAspNet10Scaffold.Data.Dto;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Models;
using RestWithAspNet10Scaffold.Repositories;

namespace RestWithAspNet10Scaffold.Services.Impl;

public class PersonService : IPersonServices
{
    private IPersonRepository _personRepository;
    private readonly PersonConverter _personConverter;
    
    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
        _personConverter = new PersonConverter();
    }
    
    public PersonDto Create(PersonDto personDto)
    {
        Person person = _personConverter.Parser(personDto);
        return  _personConverter.Parser(_personRepository.Create(person));
    }

    public PersonDto? FindById(long id)
    {
        return _personConverter.Parser(_personRepository.FindById(id));
    }

    public List<PersonDto> FindAll()
    {
        return _personConverter.ParserList(_personRepository.FindAll());
    }

    public PersonDto? Update(PersonDto personDto)
    {
        Person person = _personConverter.Parser(personDto);
        return  _personConverter.Parser(_personRepository.Update(person));
    }

    public void Delete(long id)
    {
        var person = _personRepository.FindById(id);
        if (person == null) return;
        _personRepository.Delete(person);
    }
    
    public PersonDto? Disable(long id)
    {
        var person = _personRepository.Disable(id);
        return person.Adapt<PersonDto>();
    }
    
    public PersonDto? Enable(long id)
    {
        var person = _personRepository.Enable(id);
        return person.Adapt<PersonDto>();
    }

    public List<PersonDto> FindByName(string? firstName, string? lastName)
    {
        var persons = _personRepository.FindByName(firstName, lastName);
        return _personConverter.ParserList(persons);
    }
}