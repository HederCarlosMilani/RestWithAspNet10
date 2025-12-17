using Mapster;
using RestWithAspNet10Scaffold.Data.Dto.V1;
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
    
    public List<BookDto> FindAll()
    {
        _logger.LogInformation("Find All Books");
        return _bookRepository.FindAll().Adapt<List<BookDto>>();
    }

    public BookDto? FindById(long id)
    {
        _logger.LogInformation("Find Book By Id: {Id}", id);
        return _bookRepository.FindById(id).Adapt<BookDto>();
    }
    
    public BookDto? Create(BookDto book)
    {
        _logger.LogInformation("Create Book: {Book}", book);

        try
        {
            Book createdBook = _bookRepository.Create(book.Adapt<Book>());
            return createdBook.Adapt<BookDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating book: {Book}", book);
            return null;
        }
    }
    
    public BookDto? Update(BookDto book)
    {
        _logger.LogInformation("Update Book: {Book}", book);
        try
        {
            Book updatedBook = _bookRepository.Update(book.Adapt<Book>());
            return updatedBook.Adapt<BookDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating book: {Book}", book);
            return null;
        }
    }
    
    public BookDto? Delete(long id)
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
            return existingBook.Adapt<BookDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting book: Id {Id}", id);
            return null;
        }
    }
}