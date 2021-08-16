using System;
using Microsoft.Extensions.Logging;

namespace Pdam.Common.Shared.Logging
{
    public interface IApiLogger
    {
        void Information<T>(T contents, string eventId = "0");
        void Exception(Exception ex, string eventId = "0");
        void Exception(string message, Exception ex, string eventId);
        void Exception<T>(string message, Exception ex, T content, string eventId = "0");
        void Debug<T>(string message, T content, string eventId = "0");
        void Debug<T>(string message, string eventId = "0");
        void Debug<T>(T content, string eventId = "0");
        void Information(string message);
    }
}