using RestWithAspNet10Scaffold.Models;
using RestWithAspNet10Scaffold.Repositories;

namespace RestWithAspNet10Scaffold.Services.Impl;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<BookService> _logger;
    
    public BookService(IBookRepository bookRepository, ILogger<BookService> logger)
    {
        _bookRepository = bookRepository;
        _logger = logger;
    }
    
    public List<Book> FindAll()
    {
        _logger.LogInformation("Find All Books");
        return _bookRepository.FindAll();
    }
}