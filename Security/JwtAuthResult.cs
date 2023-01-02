namespace Pdam.Common.Shared.Security;

/// <summary>
/// 
/// </summary>
public class JwtAuthResult
{
    /// <summary>
    /// 
    /// </summary>
    public string AccessToken { get; set; }= null!;

    /// <summary>
    /// 
    /// </summary>
    public RefreshToken RefreshToken { get; set; }= null!;
}

/// <summary>
/// 
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// 
    /// </summary>
    public string UserName { get; set; }   = null!;

    /// <summary>
    /// 
    /// </summary>
    public string TokenString { get; set; }= null!;

    /// <summary>
    /// 
    /// </summary>
    public DateTime ExpireAt { get; set; }
}