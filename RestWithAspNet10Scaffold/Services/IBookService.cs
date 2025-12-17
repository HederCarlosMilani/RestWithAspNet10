using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Services;

public interface IBookService
{
    List<Book> FindAll();
    Book? FindById(int id);
    Book? Create(Book book);
}