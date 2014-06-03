using System;
using AtlassianStashSharp.Models;
using System.Collections.Generic;

namespace AtlassianStashSharp.Controllers
{
    public class BuildStatusController : BaseController
    {
        public CommitBuildStatusController this[string commitId]
        {
            get { return new CommitBuildStatusController(Stash, this, commitId); }
        }

        public BuildStatusController(StashClient stash) 
            : base(stash, null)
        {
        }

        public override string Url
        {
            get { return "/rest/build-status/1.0/commits"; }
        }
    }

    public class CommitBuildStatusController : BaseController
    {
        public string Commit { get; private set; }

        public CommitBuildStatusController(StashClient stash, BaseController parent, string commit) 
            : base(stash, parent)
        {
            Commit = commit;
        }

        public StashPaginatedRequest<BuildStatus> GetStatus()
        {
            return new StashPaginatedRequest<BuildStatus>((start, limit, cancellationToken) =>
                Stash.Get<Pagination<BuildStatus>>(Url, new Dictionary<string, object>
                {
                    {"limit", limit},
                    {"start", start},
                }, cancellationToken: cancellationToken));
        }

        public override string Url
        {
            get { return Parent.Url + "/" + Commit; }
        }
    }
}

