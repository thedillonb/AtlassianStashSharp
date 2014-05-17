using System.Collections.Generic;
using AtlassianStashSharp.Models;

namespace AtlassianStashSharp.Controllers
{
    public class BranchesController : BaseController
    {
        public BranchesController(StashClient stash, BaseController parent) 
            : base(stash, parent)
        {
        }

        public StashRequest<Branch> GetDefault()
        {
            return new StashRequest<Branch>(token => Stash.Get<Branch>(Url + "/default", cancellationToken: token));
        }

        public StashPaginatedRequest<Branch> GetAll(string branch = null, bool? details = null, string filterText = null, string orderBy = null)
        {
            return new StashPaginatedRequest<Branch>((start, limit, token) => 
                Stash.Get<Pagination<Branch>>(Url, new Dictionary<string, object>
            {
                {"start", start},
                {"limit", limit},
                {"branch", branch},
                {"details", details},
                {"filterText", filterText},
                {"orderBy", orderBy}
            }, cancellationToken: token));
        }

        public override string Url
        {
            get { return Parent.Url + "/branches"; }
        }
    }
}
