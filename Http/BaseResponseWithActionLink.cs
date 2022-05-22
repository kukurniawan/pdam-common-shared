

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Http
    // ReSharper restore CheckNamespace
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