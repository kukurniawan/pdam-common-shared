using System;

namespace Pdam.Common.Shared.Security;

public class JwtAuthResult
{
    public string AccessToken { get; set; }= null!;

    public RefreshToken RefreshToken { get; set; }= null!;
}

public class RefreshToken
{
    public string UserName { get; set; }   = null!;

    public string TokenString { get; set; }= null!;

    public DateTime ExpireAt { get; set; }
}