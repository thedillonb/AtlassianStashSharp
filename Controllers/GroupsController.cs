using System.Collections.Generic;
using AtlassianStashSharp.Models;

namespace AtlassianStashSharp.Controllers
{
    public class GroupsController : BaseController
    {
        internal GroupsController(StashClient stash) 
            : base(stash, null)
        {
        }

        public StashPaginatedRequest<string> GetAll()
        {
            return new StashPaginatedRequest<string>((start, limit, token) =>
                Stash.Get<Pagination<string>>(Url, new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit}
                }, cancellationToken: token));
        }

        public override string Url
        {
            get { return "/groups"; }
        }
    }
}
