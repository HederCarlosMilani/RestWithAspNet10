using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Repositories;

public interface IBookRepository
{
     List<Book> FindAll();
     Book? FindById(long id);
     Book Create(Book book);
     Book Update(Book existingBook);
     void Delete(Book book);
}