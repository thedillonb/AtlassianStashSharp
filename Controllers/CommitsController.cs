using System.Collections.Generic;
using AtlassianStashSharp.Models;

namespace AtlassianStashSharp.Controllers
{
    public class CommitsController : BaseController
    {
        public CommitController this[string commitId]
        {
            get { return new CommitController(Stash, this, commitId); }
        }

        internal CommitsController(StashClient stash, BaseController parent) 
            : base(stash, parent)
        {
        }

        public StashPaginatedRequest<Commit> GetAll(string path = null, string since = null, string until = null, bool? withCounts = null)
        {
            return new StashPaginatedRequest<Commit>((start, limit, cancellationToken) =>
                Stash.Get<Pagination<Commit>>(Url, new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit},
                    {"path", path},
                    {"since", since},
                    {"until", until},
                    {"withCounts", withCounts}
                }, cancellationToken: cancellationToken));
        }

        public override string Url
        {
            get { return Parent.Url + "/commits"; }
        }
    }

    public class CommitController : BaseController
    {
        public string CommitId { get; private set; }

        public CommitController(StashClient stash, BaseController parent, string commitId) 
            : base(stash, parent)
        {
            CommitId = commitId;
        }

        public StashRequest<Commit> Get(string path = null)
        {
            return new StashRequest<Commit>(cancellationToken =>
                Stash.Get<Commit>(Url, new Dictionary<string, object>
                {
                    {"path", path}
                }, cancellationToken: cancellationToken));
        }

        public StashPaginatedRequest<Change> GetAllChanges(string since = null, bool? withComments = null)
        {
            return new StashPaginatedRequest<Change>((start, limit, cancellationToken) =>
                Stash.Get<Pagination<Change>>(Url + "/changes", new Dictionary<string, object>
                {
                    {"limit", limit},
                    {"start", start},
                    {"since", since},
                    {"withComments", withComments}
                }, cancellationToken: cancellationToken));
        }

        public StashRequest Watch(bool value = true)
        {
            var url = Url + "/watch";
            return value ? new StashRequest(cancellationToken => Stash.Post<string>(url, null, cancellationToken: cancellationToken)) :
                           new StashRequest(cancellationToken => Stash.Delete(url, cancellationToken: cancellationToken));
        }

        public StashRequest<Diff> GetDiff(string path = null, int contextLines = -1, string srcPath = null, string whitespace = null, bool? withComments = null)
        {
            var url = Url + "/diff";
            if (!string.IsNullOrEmpty(path))
                url = url + "/" + path;

            return new StashRequest<Diff>(token => 
                Stash.Get<Diff>(url, new Dictionary<string, object>
                {
                    {"contextLines", contextLines},
                    {"srcPath", srcPath},
                    {"whitespace", whitespace},
                    {"withComments", withComments},
                }, cancellationToken: token));
        }

        public override string Url
        {
            get { return Parent.Url + "/" + CommitId; }
        }
    }
}
