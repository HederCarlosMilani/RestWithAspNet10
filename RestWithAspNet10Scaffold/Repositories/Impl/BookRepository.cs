using RestWithAspNet10Scaffold.Contexts;
using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Repositories.Impl;

public class BookRepository : IBookRepository
{
    private readonly MSSQLContext _mssqlContext;
    
    public BookRepository(MSSQLContext mssqlContext)
    {
        _mssqlContext = mssqlContext;
    }
    
    public List<Book> FindAll()
    {
        return _mssqlContext.Books.ToList();
    }
    
    public Book? FindById(int id)
    {
        return _mssqlContext.Books.FirstOrDefault(b => b.Id == id);
    }
    
    public Book Create(Book book)
    {
        _mssqlContext.Books.Add(book);
        _mssqlContext.SaveChanges();
        return book;
    }
}