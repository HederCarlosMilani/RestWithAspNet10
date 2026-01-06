using RestWithAspNet10Scaffold.Data.Dto;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Services;

public interface IBookService
{
    List<BookDto> FindAll();
    BookDto? FindById(long id);
    BookDto? Create(BookDto book);
    BookDto? Update(BookDto book);
    BookDto? Delete(long id);
}