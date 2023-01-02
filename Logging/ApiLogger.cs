using Newtonsoft.Json;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Logging
    // ReSharper restore CheckNamespace
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiLogger : IApiLogger
    {
        private readonly JsonSerializerSettings _settings;

        /// <summary>
        /// 
        /// </summary>
        public ApiLogger()
        {
            _settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = 2
            };
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="eventId"></param>
        /// <typeparam name="T"></typeparam>
        public void Information<T>(T contents, string eventId = "0")
        {
            var json = JsonConvert.SerializeObject(contents,_settings);
            Serilog.Log.Information(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="eventId"></param>
        public void Exception(Exception ex, string eventId = "0")
        {
            Serilog.Log.Fatal(ex, ex.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="eventId"></param>
        public void Exception(string message, Exception ex, string eventId)
        {
            Serilog.Log.Fatal(ex, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="content"></param>
        /// <param name="eventId"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="NotImplementedException"></exception>
        public void Exception<T>(string message, Exception ex, T content, string eventId = "0")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="content"></param>
        /// <param name="eventId"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="NotImplementedException"></exception>
        public void Debug<T>(string message, T content, string eventId = "0")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="NotImplementedException"></exception>
        public void Debug<T>(string message, string eventId = "0")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="eventId"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="NotImplementedException"></exception>
        public void Debug<T>(T content, string eventId = "0")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Information(string message)
        {
            Serilog.Log.Information(message);
        }
    }
}