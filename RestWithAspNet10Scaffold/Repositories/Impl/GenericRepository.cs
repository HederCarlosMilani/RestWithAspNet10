using Microsoft.EntityFrameworkCore;
using RestWithAspNet10Scaffold.Contexts;
using RestWithAspNet10Scaffold.Models.Base;

namespace RestWithAspNet10Scaffold.Repositories.Impl;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    protected MSSQLContext _mssqlContext;
    private DbSet<T> _dbSet;
    public GenericRepository(MSSQLContext mssqlContext)
    {
        _mssqlContext = mssqlContext;
        _dbSet = _mssqlContext.Set<T>();
    }
    
    public List<T> FindAll()
    {
        return _dbSet.ToList();
    }

    public T? FindById(long id)
    {
        return _dbSet.FirstOrDefault(e => e.Id == id);
    }

    public T Create(T item)
    {
        _dbSet.Add(item);
        _mssqlContext.SaveChanges();
        return item;
    }

    public T? Update(T item)
    {
        var existingItem = _dbSet.FirstOrDefault(e => e.Id == item.Id);
        if (existingItem == null) return null;

        _mssqlContext.Entry(existingItem).CurrentValues.SetValues(item);
        _mssqlContext.SaveChanges();
        return item;
    }

    public void Delete(T item)
    {
        _dbSet.Remove(item);
        _mssqlContext.SaveChanges();
    }

    public bool Exists(long id)
    { 
        return _dbSet.Any(e => e.Id == id);
    }

    public List<T> FindWithPagedSearch(string query)
    {
        // return _dbSet.FromSqlRaw(query).ToList();
        return [.. _dbSet.FromSqlRaw(query)];
    }

    public int GetCount(string query)
    {
        using var connection = _mssqlContext.Database.GetDbConnection();
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText = query;
        
        var result = command.ExecuteScalar();
        return Convert.ToInt32(result);
    }
}