using RestWithAspNet10Scaffold.Contexts;
using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Repositories.Impl;

public class PersonRepository(MSSQLContext mssqlContext) : GenericRepository<Person>(mssqlContext), IPersonRepository
{
    public Person? Disable(long id)
    {
        var person = _mssqlContext.Persons.Find(id);
        if (person == null) return null;
        
        person.Enabled = false;
        _mssqlContext.SaveChanges();
        return person;
    }

    public Person? Enable(long id)
    {
        var person = _mssqlContext.Persons.Find(id);
        if (person == null) return null;
        
        person.Enabled = true;
        _mssqlContext.SaveChanges();
        return person;
    }

    public List<Person> FindByName(string? firstName, string? lastName)
    {
        var query = _mssqlContext.Persons.AsQueryable();
        if (!string.IsNullOrWhiteSpace(firstName))
            query = query.Where(p => p.FirstName.ToLower().Contains(firstName.ToLower()));
        
        if (!string.IsNullOrWhiteSpace(lastName))
            query = query.Where(p => p.LastName.ToLower().Contains(lastName.ToLower()));
        
        //return query.ToList();
        return [.. query];
    }
}