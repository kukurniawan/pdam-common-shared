using System.Collections.Generic;

namespace Pdam.Common.Shared.Http
{
    public class BaseResponseWithActionLink : BaseResponse
    {
        public Dictionary<string, string> Links { get; set; }

        public void AddLink(string name, string value)
        {
            Links ??= new Dictionary<string, string>();
            Links.Add(name, value);
        }
    }
}