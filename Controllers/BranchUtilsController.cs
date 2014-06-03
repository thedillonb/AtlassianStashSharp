using System;
using AtlassianStashSharp.Models;
using System.Collections.Generic;

namespace AtlassianStashSharp.Controllers
{
    public class BranchUtilsController : BaseController
    {
        public BranchUtilsController(StashClient stash) 
            : base(stash, null)
        {
        }

        public StashPaginatedRequest<Branch> GetBranches(string project, string repository, string commit)
        {
            var url = Url + "/projects/" + project + "/repos/" + repository + "/branches/info/" + commit;
            return new StashPaginatedRequest<Branch>((start, limit, cancellationToken) =>
                Stash.Get<Pagination<Branch>>(url, new Dictionary<string, object>
            {
                {"limit", limit},
                {"start", start},
            }, cancellationToken: cancellationToken));
        }

        public override string Url
        {
            get { return "/rest/branch-utils/1.0"; }
        }
    }
}

