using RestWithAspNet10Scaffold.Contexts;

namespace RestWithAspNet10Scaffold.Repositories.Impl;

public class BookRepository : IBookRepository
{
    private readonly MSSQLContext _mssqlContext;
    
    public BookRepository(MSSQLContext mssqlContext)
    {
        _mssqlContext = mssqlContext;
    }
}