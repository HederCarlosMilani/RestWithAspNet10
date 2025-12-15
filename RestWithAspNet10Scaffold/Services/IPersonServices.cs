using RestWithAspNet10Scaffold.Model;

namespace RestWithAspNet10Scaffold.Service;

public interface IPersonServices
{
    Person Create(Person person);
    Person? FindById(long id);
    List<Person> FindAll();
    Person? Update(Person person);
    void Delete(long id);
}