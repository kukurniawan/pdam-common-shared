using System;
using Sentry;

namespace Pdam.Common.Shared.Fault;

/// <summary>
/// 
/// </summary>
public class SentryService : ISentryService
{
    private readonly string _releaseVersion;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="releaseVersion"></param>
    public SentryService(string releaseVersion)
    {
        _releaseVersion = releaseVersion;
    }

    /// <summary>
    /// 
    /// </summary>
    public void CaptureException(Exception e)
    {
        using var g = SentrySdk.Init(o =>
        {
            o.Dsn = Environment.GetEnvironmentVariable("SENTRY_DSN");
            o.Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            o.Debug = true;
            o.Release = _releaseVersion;
        });
        SentrySdk.CaptureException(e);
    }

    public void CaptureMessage(string message)
    {
        using var g = SentrySdk.Init(o =>
        {
            o.Dsn = Environment.GetEnvironmentVariable("SENTRY_DSN");
            o.Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            o.Debug = true;
            o.Release = _releaseVersion;
        });
        SentrySdk.CaptureMessage(message);
    }
}

/// <summary>
/// 
/// </summary>
public interface ISentryService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    void CaptureException(Exception e);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    void CaptureMessage(string message);
}