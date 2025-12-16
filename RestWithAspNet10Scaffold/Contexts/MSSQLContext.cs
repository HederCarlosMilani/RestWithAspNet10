using Microsoft.EntityFrameworkCore;
using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Contexts;

public class MSSQLContext : DbContext
{
    public MSSQLContext(DbContextOptions<MSSQLContext> options) : base(options) { }
    
    public DbSet<Person> Persons { get; set; }
}