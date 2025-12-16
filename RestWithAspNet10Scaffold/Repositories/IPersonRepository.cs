using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Repositories;

public interface IPersonRepository
{
    Person? FindById(long id);
    List<Person> GetAll();
    Person Create(Person person);
    Person? Update(Person person);
    void Delete(long id);
}