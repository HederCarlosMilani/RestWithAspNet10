using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Models;
using RestWithAspNet10Scaffold.Services;

namespace RestWithAspNet10Scaffold.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : Controller
{   
    private readonly IBookService _bookService;
    
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        List<Book> books = _bookService.FindAll();
        return books.Count == 0 ? NotFound() : Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        Book? book = _bookService.FindById(id);
        return book == null ? NotFound() : Ok(book);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Book book)
    {
        Book? createdBook = _bookService.Create(book);
        return createdBook == null
            ? BadRequest("Problema na criação do usuário")
            : CreatedAtAction(nameof(Get), new { id = createdBook.Id }, createdBook);
    }

    [HttpPut]
    public IActionResult Put([FromBody] Book book)
    {
        Book? updatedBook = _bookService.Update(book);
        return updatedBook == null ? NotFound() : Ok(updatedBook);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        Book? book = _bookService.Delete(id);
        return book == null ? NotFound() : NoContent();
    }
}