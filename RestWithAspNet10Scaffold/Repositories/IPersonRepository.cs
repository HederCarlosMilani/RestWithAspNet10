using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Repositories;

public interface IPersonRepository : IRepository<Person>
{
    Person? Disable(long id);
    Person? Enable(long id);
}