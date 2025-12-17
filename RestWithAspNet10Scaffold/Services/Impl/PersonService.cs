using RestWithAspNet10Scaffold.Data.Convert.Impl;
using RestWithAspNet10Scaffold.Data.Dto;
using RestWithAspNet10Scaffold.Models;
using RestWithAspNet10Scaffold.Repositories;

namespace RestWithAspNet10Scaffold.Services.Impl;

public class PersonService : IPersonServices
{
    private IRepository<Person> _personRepository;
    private readonly PersonConverter _personConverter;
    
    public PersonService(IRepository<Person> personRepository)
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
}