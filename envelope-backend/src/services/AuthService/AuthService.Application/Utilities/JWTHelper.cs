using AuthService.Application.Config;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthService.Application.Utilities;

public class JWTHelper
{
    public static string CreateJWT(string username, string id)
    {
        var claims = new List<Claim> { 
            new(ClaimTypes.Name, username),
            new("Id", id)
        };

        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public static Dictionary<string, string> GetClaimsFromToken(string token)
    {
        var claimsDict = new Dictionary<string, string>();
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        foreach (var claim in jwtSecurityToken.Claims)
        {
            claimsDict[claim.Type] = claim.Value;
        }
        
        return claimsDict;
    }
}