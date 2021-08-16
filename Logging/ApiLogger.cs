using System;
using Newtonsoft.Json;

namespace Pdam.Common.Shared.Logging
{
    public class ApiLogger : IApiLogger
    {
        private readonly JsonSerializerSettings _settings;

        public ApiLogger()
        {
            _settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = 2
            };
        }
        
        public void Information<T>(T contents, string eventId = "0")
        {
            var json = JsonConvert.SerializeObject(contents,_settings);
            Serilog.Log.Information(json);
        }

        public void Exception(Exception ex, string eventId = "0")
        {
            Serilog.Log.Fatal(ex, ex.Message);
        }

        public void Exception(string message, Exception ex, string eventId)
        {
            Serilog.Log.Fatal(ex, message);
        }

        public void Exception<T>(string message, Exception ex, T content, string eventId = "0")
        {
            throw new NotImplementedException();
        }

        public void Debug<T>(string message, T content, string eventId = "0")
        {
            throw new NotImplementedException();
        }

        public void Debug<T>(string message, string eventId = "0")
        {
            throw new NotImplementedException();
        }

        public void Debug<T>(T content, string eventId = "0")
        {
            throw new NotImplementedException();
        }

        public void Information(string message)
        {
            Serilog.Log.Information(message);
        }
    }
}