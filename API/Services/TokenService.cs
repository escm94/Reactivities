using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
  public class TokenService
  {
    private readonly IConfiguration _config;
    public TokenService(IConfiguration config)
    {
      _config = config;

    }

    public string CreateToken(AppUser user)
    {

      // we'll use these claims at various points to either get a user from the database or establish if user is who they say they are
      var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };

      // TokenKey configured in appSettings.Development.json
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"])); // requires 12+ chars, ideally longer
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); //because our token has to be signed using this symmetric key   

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = creds,
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}