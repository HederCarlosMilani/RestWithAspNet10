using RestWithAspNet10Scaffold.Contexts;
using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Repositories.Impl;

public class PersonRepository(MSSQLContext mssqlContext) : GenericRepository<Person>(mssqlContext), IPersonRepository
{
    public Person? Disable(int id)
    {
        var person = _mssqlContext.Persons.Find(id);
        if (person == null) return null;
        
        person.Enabled = false;
        _mssqlContext.SaveChanges();
        return person;
    }

    public Person? Enable(int id)
    {
        var person = _mssqlContext.Persons.Find(id);
        if (person == null) return null;
        
        person.Enabled = true;
        _mssqlContext.SaveChanges();
        return person;
    }
}