using RestWithAspNet10Scaffold.Contexts;
using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Repositories.Impl;

public class PersonRepository : IPersonRepository
{
    private readonly MSSQLContext _context;
    
    public PersonRepository(MSSQLContext context)
    {
        _context = context;
    }
    
    public Person? FindById(long id)
    {
        return  _context.Persons.FirstOrDefault(p => p.Id == id);
    }

    public List<Person> GetAll()
    {
        return  _context.Persons.ToList();
    }

    public Person Create(Person person)
    {
        _context.Persons.Add(person);
        _context.SaveChanges();
        return person;
    }

    public Person? Update(Person person)
    {
        var existingPerson = _context.Persons.FirstOrDefault(p => p.Id == person.Id);
        if (existingPerson == null) return null;

        existingPerson.FirstName = person.FirstName;
        existingPerson.LastName = person.LastName;
        existingPerson.Address = person.Address;
        existingPerson.Gender = person.Gender;
        _context.SaveChanges();
        return existingPerson;
    }

    public void Delete(long id)
    {
        var person = _context.Persons.FirstOrDefault(p => p.Id == id);
        if (person != null)
        {
            _context.Persons.Remove(person);
            _context.SaveChanges();
        }
    }
}