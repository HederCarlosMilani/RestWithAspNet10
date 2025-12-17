using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto;
using RestWithAspNet10Scaffold.Data.Dto.V1;
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
        List<BookDto> books = _bookService.FindAll();
        return books.Count == 0 ? NotFound() : Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        BookDto? book = _bookService.FindById(id);
        return book == null ? NotFound() : Ok(book);
    }

    [HttpPost]
    public IActionResult Post([FromBody] BookDto book)
    {
        BookDto? createdBook = _bookService.Create(book);
        return createdBook == null
            ? BadRequest("Problema na criação do usuário")
            : CreatedAtAction(nameof(Get), new { id = createdBook.Id }, createdBook);
    }

    [HttpPut]
    public IActionResult Put([FromBody] BookDto book)
    {
        BookDto? updatedBook = _bookService.Update(book);
        return updatedBook == null ? NotFound() : Ok(updatedBook);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        BookDto? book = _bookService.Delete(id);
        return book == null ? NotFound() : NoContent();
    }
}