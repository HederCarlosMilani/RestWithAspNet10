using Microsoft.AspNetCore.Mvc;
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
}