using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthService.Application.Config;

public class AuthOptions
{
    public const string ISSUER = "EnvelopeBack";
    public const string AUDIENCE = "EnvelopeClient";
    const string KEY = "prostokeyfor1234231dsfsdq3shiphrovki014345";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}