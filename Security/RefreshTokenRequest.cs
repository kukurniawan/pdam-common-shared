using System;
using MediatR;

namespace Pdam.Common.Shared.Security;

/// <summary>
/// 
/// </summary>
public class RefreshTokenRequest : IRequest<LoginResponse>
{
    /// <summary>
    /// 
    /// </summary>
    public string RefreshToken { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Guid Uid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string CompanyId { get; set; }
}