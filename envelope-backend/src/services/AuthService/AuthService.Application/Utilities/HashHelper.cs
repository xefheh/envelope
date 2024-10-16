using System.Security.Cryptography;
using System.Text;

namespace AuthService.Application.Utilities;

public class HashHelper
{
    public static string CalculateMD5HashForString(string text)
    {
        var bytesText = Encoding.ASCII.GetBytes(text);
        var hashText = MD5.HashData(bytesText);

        return string.Join(string.Empty, hashText.Select(e => e.ToString("X2")));
    }
}