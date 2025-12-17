using RestWithAspNet10Scaffold.Models.Base;

namespace RestWithAspNet10Scaffold.Repositories.Impl;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    public List<T> FindAll()
    {
        throw new NotImplementedException();
    }

    public T? FindById(long id)
    {
        throw new NotImplementedException();
    }

    public T Create(T item)
    {
        throw new NotImplementedException();
    }

    public T Update(T existingItem)
    {
        throw new NotImplementedException();
    }

    public void Delete(T item)
    {
        throw new NotImplementedException();
    }

    public bool Exists(long id)
    {
        throw new NotImplementedException();
    }
}