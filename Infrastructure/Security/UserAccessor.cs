using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security
{
  public class UserAccessor : IUserAccessor
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    // use dependency injection so that we can get the user object to get the Claims inside the token
    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }
    public string GetUsername()
    {
      return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name); 
    }
  }
}