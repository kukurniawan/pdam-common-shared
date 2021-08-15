using System;
using Microsoft.Extensions.Logging;

namespace Pdam.Common.Shared.Logging
{
    public interface IApiLogger
    {
        void Log<T>(LogLevel level, EventId eventId, T state, Exception exception = null, Func<T, Exception, string> formatter = null);
        void Log<T>(string message, LogLevel level, EventId eventId, T state);
        void Information<T>(T contents, string eventId = "0");
        void Exception(Exception ex, string eventId = "0");
        void Exception(string message, Exception ex, string eventId);
        void Exception<T>(string message, Exception ex, T content, string eventId = "0");
        void Debug<T>(string message, T content, string eventId = "0");
        void Debug<T>(string message, string eventId = "0");
        void Debug<T>(T content, string eventId = "0");
        void Information(string message);
        void Information(string eventId, string message);
    }
}