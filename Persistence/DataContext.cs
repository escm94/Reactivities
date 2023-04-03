using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Persistence
{
  // by extending IdentityDbContext, we don't need to specify a new DbSet for our AppUser
  // everything's gonna be taken care of by the IdentityDbContext and it'll to scaffold a lot of tables in our database related to identity
  public class DataContext : IdentityDbContext<AppUser>
  { 
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    // with DataContexts, we create DbSets or tables essentially. 
    // we need to add the using statement for Domain to access the correct Activity object and not some other random Activity object.
    public DbSet<Activity> Activities { get; set; }
  }
}