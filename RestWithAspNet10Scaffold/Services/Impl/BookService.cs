using RestWithAspNet10Scaffold.Repositories;

namespace RestWithAspNet10Scaffold.Services.Impl;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
}