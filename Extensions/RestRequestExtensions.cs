using PortableRest;

namespace AtlassianStashSharp.Extensions
{
    internal static class RestRequestExtensions
    {
        public static RestRequest WithPagination(this RestRequest @this, int start, int limit)
        {
            @this.AddQueryString("start", start);
            @this.AddQueryString("limit", limit);
            return @this;
        }
    }
}
