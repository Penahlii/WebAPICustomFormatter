using Microsoft.EntityFrameworkCore;
using WebAPICustomFormatter.Entities;

namespace WebAPICustomFormatter.Data;

public class PersonDbContext : DbContext
{
    public PersonDbContext(DbContextOptions<PersonDbContext> options)
        : base(options) { }

    public virtual DbSet<Person> People { get; set; }
}
