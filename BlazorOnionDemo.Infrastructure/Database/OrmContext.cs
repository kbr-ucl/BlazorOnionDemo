using BlazorOnionDemo.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlazorOnionDemo.Infrastructure.Database;

public class OrmContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    public OrmContext(DbContextOptions<OrmContext> options) : base(options)
    {
    }
}