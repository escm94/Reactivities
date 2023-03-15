using Domain;
using Microsoft.EntityFrameworkCore;
namespace Persistence
{
  public class DataContext : DbContext
  { 
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    // with DataContexts, we create DbSets or tables essentially. 
    // we need to add the using statement for Domain to access the correct Activity object and not some other random Activity object.
    public DbSet<Activity> Activities { get; set; }
  }
}