using RestWithAspNet10Scaffold.Context;
using RestWithAspNet10Scaffold.Model;

namespace RestWithAspNet10Scaffold.Service.Impl;

public class PersonServiceImpl : IPersonServices
{
    private MSSQLContext _mssqlContext;
    public PersonServiceImpl(MSSQLContext mssqlContext)
    {
        _mssqlContext = mssqlContext;
    }
    
    public Person Create(Person person)
    {
        _mssqlContext.Persons.Add(person);
        _mssqlContext.SaveChanges();
        return person;
    }

    public Person? FindById(long id)
    {
        return _mssqlContext.Persons.Find(id);
    }

    public List<Person> FindAll()
    {
        var persons = _mssqlContext.Persons.ToList();
        return persons;
    }

    public Person? Update(Person person)
    {
        var existingPerson = _mssqlContext.Persons.Find(person.Id);
        if (existingPerson == null) return null;
        _mssqlContext.Entry(existingPerson).CurrentValues.SetValues(person);
        _mssqlContext.SaveChanges();
        return person;
    }

    public void Delete(long id)
    {
        var person = _mssqlContext.Persons.Find(id);
        if (person == null) return;
        _mssqlContext.Persons.Remove(person);
        _mssqlContext.SaveChanges();
    }
}