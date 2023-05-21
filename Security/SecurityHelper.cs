using System.Security.Cryptography;
using System.Text;

namespace Pdam.Common.Shared.Security;

/// <summary>
/// 
/// </summary>
public class SecurityHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="plainPassword"></param>
    /// <returns></returns>
    public static string CreateMd5Hash(string plainPassword)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        //compute hash from the bytes of text            
        md5.ComputeHash(Encoding.ASCII.GetBytes(plainPassword));
        //get hash result after compute it            
        var bytes = md5.Hash;
        var strBuilder = new StringBuilder();
        foreach (var t in bytes!)
        {
            strBuilder.Append(t.ToString("X2"));
        }
        return strBuilder.ToString();
    }
}