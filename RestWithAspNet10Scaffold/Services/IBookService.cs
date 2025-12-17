using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Services;

public interface IBookService
{
    List<Book> FindAll();
}