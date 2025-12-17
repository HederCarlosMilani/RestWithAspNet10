using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Services;

public interface IBookService
{
    List<Book> FindAll();
    Book? FindById(long id);
    Book? Create(Book book);
    Book? Update(Book book);
    Book? Delete(long id);
}