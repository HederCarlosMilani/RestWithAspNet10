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

    public Book? FindById(int id)
    {
        _logger.LogInformation("Find Book By Id: {Id}", id);
        return _bookRepository.FindById(id);
    }
    
    public Book? Create(Book book)
    {
        _logger.LogInformation("Create Book: {Book}", book);

        try
        {
            Book createdBook = _bookRepository.Create(book);
            return createdBook;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating book: {Book}", book);
            return null;
        }
    }
}