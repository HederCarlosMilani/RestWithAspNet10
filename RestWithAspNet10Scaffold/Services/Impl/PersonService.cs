using RestWithAspNet10Scaffold.Contexts;
using RestWithAspNet10Scaffold.Models;
using RestWithAspNet10Scaffold.Repositories;

namespace RestWithAspNet10Scaffold.Services.Impl;

public class PersonService : IPersonServices
{
    private readonly IRepository<Person> _personRepository;
    
    public PersonService(IRepository<Person> personRepository)
    {
        _personRepository = personRepository;
    }
    
    public Person Create(Person person)
    {
        return  _personRepository.Create(person);
    }

    public Person? FindById(long id)
    {
        return _personRepository.FindById(id);
    }

    public List<Person> FindAll()
    {
        return _personRepository.FindAll();
    }

    public Person? Update(Person person)
    {
        return  _personRepository.Update(person);
    }

    public void Delete(long id)
    {
        var person = _personRepository.FindById(id);
        if (person == null) return;
        _personRepository.Delete(person);
    }
}