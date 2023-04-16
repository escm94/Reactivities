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
    public DbSet<ActivityAttendee> ActivityAttendees { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      // now that we have access to our entity creation, let's add the primary key for it
      builder.Entity<ActivityAttendee>(x => x.HasKey(aa => new {aa.AppUserId, aa.ActivityId}));

      // establish the many to many relationship between the two tables

      builder.Entity<ActivityAttendee>()
        .HasOne(u => u.AppUser)
        .WithMany(a => a.Activities)
        .HasForeignKey(aa => aa.AppUserId);

      builder.Entity<ActivityAttendee>()
        .HasOne(u => u.Activity)
        .WithMany(a => a.Attendees)
        .HasForeignKey(aa => aa.ActivityId);
    }
  }
}