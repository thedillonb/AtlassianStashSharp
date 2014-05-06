using System.Net.Http;
using AtlassianStashSharp.Extensions;
using AtlassianStashSharp.Models;
using PortableRest;

namespace AtlassianStashSharp.Controllers
{
    public class ProfileController : BaseController
    {
        internal ProfileController(StashClient stash)
            : base(stash, null)
        {
        }

        public StashPaginatedRequest<Repository> GetRecentRepos(string permission = null)
        {
            return new StashPaginatedRequest<Repository>((start, limit) =>
            {
                var req = new RestRequest(Url, HttpMethod.Get).WithPagination(start, limit);
                if (permission != null) req.AddQueryString("permission", permission);
                return Stash.Client.ExecuteAsync<Pagination<Repository>>(req);
            });
        }

        public override string Url
        {
            get { return "/profile"; }
        }
    }
}
