namespace Pdam.Common.Shared.Helper
{
    public static class StringHelper
    {
        public static string RemoveTrailingSlash(this string value) => value.EndsWith("/") ? value.TrimEnd('/') : value;
    }
}