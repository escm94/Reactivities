using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Security
{
  public class IsHostRequirement : IAuthorizationRequirement
  {
  }

  public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
  {
    private readonly DataContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IsHostRequirementHandler(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
      _dbContext = dbContext;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
    {
      var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

      if (userId == null) return Task.CompletedTask;

      var activityId = Guid.Parse(_httpContextAccessor.HttpContext?
        .Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString());

      // need to add .Result because we can't specify this method as async since we're overriding it, we want to return completed tasks
      var attendee = _dbContext.ActivityAttendees
        .AsNoTracking()
        .SingleOrDefaultAsync(x => x.AppUserId == userId && x.ActivityId == activityId).Result; 

      if (attendee == null) return Task.CompletedTask;

      if (attendee.IsHost) context.Succeed(requirement);

      // if we return at this point, and the context.Succeed flag is set, then the user will be authorized to edit activity  
      return Task.CompletedTask;
    }
  }
}