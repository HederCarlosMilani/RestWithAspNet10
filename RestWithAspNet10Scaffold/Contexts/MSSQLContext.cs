using Microsoft.EntityFrameworkCore;
using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Contexts;

public class MSSQLContext(DbContextOptions<MSSQLContext> options) : DbContext(options)
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
}