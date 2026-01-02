using System.Security.Cryptography;
using System.Text;
using RestWithAspNet10Scaffold.Auth.Contract;

namespace RestWithAspNet10Scaffold.Auth.Tools;

public class Sha256PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = SHA256.HashData(bytes);
        
        var stringBuilder = new StringBuilder();
        foreach (var item in hash) 
            stringBuilder.Append(item.ToString("x2"));
        
        return stringBuilder.ToString();
    }

    public bool Verify(string password, string hashedPassword)
    {
        return (hashedPassword == Hash(password));
    }
}