// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Helper
    // ReSharper restore CheckNamespace
{
    public static class StringHelper
    {
        public static string RemoveTrailingSlash(this string value) => value.EndsWith("/") ? value.TrimEnd('/') : value;
    }
}