using System.Net.Http;
using AtlassianStashSharp.Extensions;
using AtlassianStashSharp.Models;
using PortableRest;

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
            return new StashPaginatedRequest<PullRequest>((start, limit) =>
            {
                var req = new RestRequest(Url, HttpMethod.Get).WithPagination(start, limit);
                if (direction != null) req.AddQueryString("direction", direction);
                if (at != null) req.AddQueryString("at", at);
                if (state != null) req.AddQueryString("state", state);
                if (order != null) req.AddQueryString("order", order);
                return Stash.Client.ExecuteAsync<Pagination<PullRequest>>(req);
            });
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
            return new StashRequest<PullRequest>(() => Stash.Client.ExecuteAsync<PullRequest>(new RestRequest(Url, HttpMethod.Get)));
        }

        public StashRequest Watch(bool value = true)
        {
            var url = Url + "/watch";
            return value ? new StashRequest(() => Stash.Client.ExecuteAsync<string>(new RestRequest(url, HttpMethod.Post))) : 
                           new StashRequest(() => Stash.Client.ExecuteAsync<string>(new RestRequest(url, HttpMethod.Delete)));
        }

        public StashRequest Decline(string version = null)
        {
            return new StashRequest(() =>
            {
                var req = new RestRequest(Url + "/decline", HttpMethod.Post);
                if (version != null) req.AddQueryString("version", version);
                return Stash.Client.ExecuteAsync<string>(req);
            });
        }

        public StashRequest Approve(bool value = true)
        {
            var url = Url + "/approve";
            return value ? new StashRequest(() => Stash.Client.ExecuteAsync<string>(new RestRequest(url, HttpMethod.Post))) :
                           new StashRequest(() => Stash.Client.ExecuteAsync<string>(new RestRequest(url, HttpMethod.Delete)));
        }

        public StashPaginatedRequest<PullRequestParticipant> GetAllParticipates()
        {
            return new StashPaginatedRequest<PullRequestParticipant>((start, limit) =>
            {
                var req = new RestRequest(Url + "/participants", HttpMethod.Get).WithPagination(start, limit);
                return Stash.Client.ExecuteAsync<Pagination<PullRequestParticipant>>(req);
            });
        }

        public StashPaginatedRequest<Commit> GetAllCommits(bool? withCounts = null)
        {
            return new StashPaginatedRequest<Commit>((start, limit) =>
            {
                var req = new RestRequest(Url + "/commits", HttpMethod.Get).WithPagination(start, limit);
                if (withCounts != null) req.AddQueryString("withCounts", withCounts.Value);
                return Stash.Client.ExecuteAsync<Pagination<Commit>>(req);
            });
        }

        public StashPaginatedRequest<Comment> GetAllComments(string path = null)
        {
            return new StashPaginatedRequest<Comment>((start, limit) =>
            {
                var req = new RestRequest(Url + "/comments", HttpMethod.Get).WithPagination(start, limit);
                if (path != null) req.AddQueryString("path", path);
                return Stash.Client.ExecuteAsync<Pagination<Comment>>(req);
            });
        }

        public StashPaginatedRequest<Change> GetAllChanges(bool? withComments = null)
        {
            return new StashPaginatedRequest<Change>((start, limit) =>
            {
                var req = new RestRequest(Url + "/changes", HttpMethod.Get).WithPagination(start, limit);
                if (withComments != null) req.AddQueryString("withComments", withComments.Value);
                return Stash.Client.ExecuteAsync<Pagination<Change>>(req);
            });
        }

        public override string Url
        {
            get { return Parent.Url + "/" + PulLRequestId; }
        }
    }
}
