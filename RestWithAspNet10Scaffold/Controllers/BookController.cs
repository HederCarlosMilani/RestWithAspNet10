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
}