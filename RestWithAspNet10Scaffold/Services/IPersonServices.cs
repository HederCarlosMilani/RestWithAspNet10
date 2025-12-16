using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Services;

public interface IPersonServices
{
    Person Create(Person person);
    Person? FindById(long id);
    List<Person> FindAll();
    Person? Update(Person person);
    void Delete(long id);
}