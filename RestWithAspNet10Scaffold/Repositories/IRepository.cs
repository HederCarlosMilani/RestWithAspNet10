using RestWithAspNet10Scaffold.Models.Base;

namespace RestWithAspNet10Scaffold.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    List<T> FindAll();
    T? FindById(long id);
    T Create(T item);
    T? Update(T item);
    void Delete(T item);
    bool Exists(long id);
}