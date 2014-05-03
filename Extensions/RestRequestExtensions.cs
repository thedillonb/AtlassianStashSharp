using PortableRest;

namespace AtlassianStashSharp.Extensions
{
    internal static class RestRequestExtensions
    {
        public static RestRequest WithPagination(this RestRequest @this, int start, int limit)
        {
            @this.AddParameter("start", start);
            @this.AddParameter("limit", limit);
            return @this;
        }
    }
}
