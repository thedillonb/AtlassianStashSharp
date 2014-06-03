using System.Collections.Generic;
using AtlassianStashSharp.Models;

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
            return new StashPaginatedRequest<Repository>((start, limit, cancellationToken) =>
                Stash.Get<Pagination<Repository>>(Url, new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit},
                    {"permission", permission}
                }, cancellationToken: cancellationToken));
        }

        public override string Url
        {
            get { return "/rest/api/1.0/profile"; }
        }
    }
}
