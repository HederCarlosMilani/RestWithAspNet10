using RestWithAspNet10Scaffold.Model;

namespace RestWithAspNet10Scaffold.Service.Impl;

public class PersonServiceImpl : IPersonServices
{
    public Person Create(Person person)
    {
        person.Id = new Random().Next(1, 10000);
        return person;
    }

    public Person FindById(long id)
    {
        var person = MockPerson(id);
        return person;
    }

    private Person MockPerson(long id)
    {
        return new Person
        {
            Id = id,
            FirstName = "John",
            LastName = "Doe",
            Address = "123 Main St, Anytown, USA",
            Gender = "Male",
        };
    }

    public List<Person> FindAll()
    {
        var persons = new List<Person>();
        for (long i = 1; i <= 8; i++)
        {
            persons.Add(MockPerson( new Random().Next(1, 10000)));
        }
        return persons;
    }

    public Person Update(Person person)
    {
        person.Id = new Random().Next(1, 10000);
        return person;
    }

    public void Delete(long id)
    {
        // Not implemented
    }
}