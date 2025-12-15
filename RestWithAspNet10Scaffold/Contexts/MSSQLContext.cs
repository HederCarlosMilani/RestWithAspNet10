using Microsoft.EntityFrameworkCore;
using RestWithAspNet10Scaffold.Model;

namespace RestWithAspNet10Scaffold.Context;

public class MSSQLContext : DbContext
{
    public MSSQLContext(DbContextOptions<MSSQLContext> options) : base(options) { }
    
    public DbSet<Person> Persons { get; set; }
}