using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Repositories;

public interface IBookRepository
{
     List<Book> FindAll();
     Book? FindById(int id);
     Book Create(Book book);
}