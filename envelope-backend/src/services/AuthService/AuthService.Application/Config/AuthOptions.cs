using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthService.Application.Config;

public class AuthOptions
{
    public const string Issuer = "EnvelopeBack";
    public const string Audience = "EnvelopeClient";
    private const string Key = "prostokeyfor1234231dsfsdq3shiphrovki014345";

    public static SymmetricSecurityKey GetSymmetricSecurityKey() {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}