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
    
    public Book? FindById(long id)
    {
        return _mssqlContext.Books.FirstOrDefault(b => b.Id == id);
    }
    
    public Book Create(Book book)
    {
        _mssqlContext.Books.Add(book);
        _mssqlContext.SaveChanges();
        return book;
    }
    
    public Book Update(Book existingBook)
    {
        var book = _mssqlContext.Books.FirstOrDefault(b => b.Id == existingBook.Id);
        if (book == null) return null;

        book.Title = existingBook.Title;
        book.Author = existingBook.Author;
        book.Price = existingBook.Price;
        _mssqlContext.SaveChanges();
        return book;
    }

    public void Delete(Book book)
    {
        _mssqlContext.Books.Remove(book);
        _mssqlContext.SaveChanges();
    }
}