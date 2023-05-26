

// ReSharper disable CheckNamespace

using System;

namespace Pdam.Common.Shared.Logging
    // ReSharper restore CheckNamespace
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApiLogger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="eventId"></param>
        /// <typeparam name="T"></typeparam>
        void Information<T>(T contents, string eventId = "0");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="eventId"></param>
        void Exception(Exception ex, string eventId = "0");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="eventId"></param>
        void Exception(string message, Exception ex, string eventId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="content"></param>
        /// <param name="eventId"></param>
        /// <typeparam name="T"></typeparam>
        void Exception<T>(string message, Exception ex, T content, string eventId = "0");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="content"></param>
        /// <param name="eventId"></param>
        /// <typeparam name="T"></typeparam>
        void Debug<T>(string message, T content, string eventId = "0");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <typeparam name="T"></typeparam>
        void Debug<T>(string message, string eventId = "0");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="eventId"></param>
        /// <typeparam name="T"></typeparam>
        void Debug<T>(T content, string eventId = "0");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Information(string message);
    }
}