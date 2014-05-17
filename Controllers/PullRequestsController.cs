using System.Collections.Generic;
using AtlassianStashSharp.Models;

namespace AtlassianStashSharp.Controllers
{
    public class PullRequestsController : BaseController
    {
        public PullRequestController this[long pullRequestId]
        {
            get { return new PullRequestController(Stash, this, pullRequestId); }
        }

        internal PullRequestsController(StashClient stash, BaseController parent) 
            : base(stash, parent)
        {
        }

        public StashPaginatedRequest<PullRequest> GetAll(string direction = null, string at = null, string state = null, string order = null)
        {
            return new StashPaginatedRequest<PullRequest>((start, limit, token) =>
                Stash.Get<Pagination<PullRequest>>(Url, new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit},
                    {"direction", direction},
                    {"at", at},
                    {"state", state},
                    {"order", order}
                }, cancellationToken: token));
        }

        public override string Url
        {
            get { return Parent.Url + "/pull-requests"; }
        }
    }

    public class PullRequestController : BaseController
    {
        public long PulLRequestId { get; private set; }

        internal PullRequestController(StashClient stash, BaseController parent, long pulLRequestId) 
            : base(stash, parent)
        {
            PulLRequestId = pulLRequestId;
        }

        public StashRequest<PullRequest> Get()
        {
            return new StashRequest<PullRequest>(token => Stash.Get<PullRequest>(Url, cancellationToken: token));
        }

        public StashRequest Watch(bool value = true)
        {
            var url = Url + "/watch";
            return value ? new StashRequest(token => Stash.Post<string>(url, null, cancellationToken: token)) : 
                           new StashRequest(token => Stash.Delete(url, cancellationToken: token));
        }

        public StashRequest Decline(string version = null)
        {
            return new StashRequest(token =>
                Stash.Post<string>(Url + "/decline", null, new Dictionary<string, object>
                {
                    {"version", version}
                }, cancellationToken: token));
        }

        public StashRequest Approve(bool value = true)
        {
            var url = Url + "/approve";
            return value ? new StashRequest(token => Stash.Post<string>(url, null, cancellationToken: token)) :
                           new StashRequest(token => Stash.Delete(url, cancellationToken: token));
        }

        public StashPaginatedRequest<PullRequestParticipant> GetAllParticipates()
        {
            return new StashPaginatedRequest<PullRequestParticipant>((start, limit, token) =>
                Stash.Get<Pagination<PullRequestParticipant>>(Url + "/participants", new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit}
                }, cancellationToken: token));
        }

        public StashPaginatedRequest<Commit> GetAllCommits(bool? withCounts = null)
        {
            return new StashPaginatedRequest<Commit>((start, limit, token) =>
                Stash.Get<Pagination<Commit>>(Url + "/commits", new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit},
                    {"withCounts", withCounts}
                }, cancellationToken: token));
        }

        public StashPaginatedRequest<Comment> GetAllComments(string path = null)
        {
            return new StashPaginatedRequest<Comment>((start, limit, token) =>
                Stash.Get<Pagination<Comment>>(Url + "/comments", new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit},
                    {"path", path}
                }, cancellationToken: token));
        }

        public StashPaginatedRequest<Change> GetAllChanges(bool? withComments = null)
        {
            return new StashPaginatedRequest<Change>((start, limit, token) =>
                Stash.Get<Pagination<Change>>(Url + "/changes", new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit},
                    {"withComments", withComments}
                }, cancellationToken: token));
        }

        public override string Url
        {
            get { return Parent.Url + "/" + PulLRequestId; }
        }
    }
}
