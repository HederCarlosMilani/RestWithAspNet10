using RestWithAspNet10Scaffold.Models;
using RestWithAspNet10Scaffold.Repositories;

namespace RestWithAspNet10Scaffold.Services.Impl;

public class BookService : IBookService
{
    private readonly IRepository<Book> _bookRepository;
    private readonly ILogger<BookService> _logger;
    
    public BookService(IRepository<Book> bookRepository, ILogger<BookService> logger)
    {
        _bookRepository = bookRepository;
        _logger = logger;
    }
    
    public List<Book> FindAll()
    {
        _logger.LogInformation("Find All Books");
        return _bookRepository.FindAll();
    }

    public Book? FindById(long id)
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
    
    public Book? Update(Book book)
    {
        _logger.LogInformation("Update Book: {Book}", book);
        try
        {
            Book updatedBook = _bookRepository.Update(book);
            return updatedBook;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating book: {Book}", book);
            return null;
        }
    }
    
    public Book? Delete(long id)
    {
        _logger.LogInformation("Delete Book By Id: {Id}", id);
        Book? existingBook = _bookRepository.FindById(id);
        if (existingBook == null)
        {
            _logger.LogWarning("Book not found for deletion: Id {Id}", id);
            return null;
        }

        try
        {
            _bookRepository.Delete(existingBook);
            return existingBook;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting book: Id {Id}", id);
            return null;
        }
    }
}