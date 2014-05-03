using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AtlassianStashSharp.Extensions;
using AtlassianStashSharp.Models;
using PortableRest;

namespace AtlassianStashSharp.Controllers
{
    public class CommitsController : BaseController
    {
        internal CommitsController(StashClient stash, BaseController parent) 
            : base(stash, parent)
        {
        }

        public StashPaginatedRequest<Commit> GetAll(string path = null, string since = null, string until = null, bool? withCounts = null)
        {
            return new StashPaginatedRequest<Commit>((start, limit) =>
            {
                var req = new RestRequest(Url, HttpMethod.Get).WithPagination(start, limit);
                if (path != null) req.AddParameter("path", path);
                if (since != null) req.AddParameter("since", since);
                if (until != null) req.AddParameter("until", until);
                if (withCounts != null) req.AddParameter("withCounts", withCounts.Value);

                return Stash.Client.ExecuteAsync<Pagination<Commit>>(req);
            });
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
            return new StashRequest<Commit>(() =>
            {
                var req = new RestRequest(Url);
                if (path != null) req.AddParameter("path", path);
                return Stash.Client.ExecuteAsync<Commit>(req);
            });
        }

        public StashPaginatedRequest<Change> GetAllChanges(string since = null, bool? withComments = null)
        {
            return new StashPaginatedRequest<Change>((start, limit) =>
            {
                var req = new RestRequest(Url + "/changes", HttpMethod.Get).WithPagination(start, limit);
                if (since != null) req.AddParameter("since", since);
                if (withComments != null) req.AddParameter("withComments", withComments.Value);
                return Stash.Client.ExecuteAsync<Pagination<Change>>(req);
            });
        }

        public StashRequest Watch(bool value = true)
        {
            var url = Url + "/watch";
            return value ? new StashRequest(() => Stash.Client.ExecuteAsync<string>(new RestRequest(url, HttpMethod.Post))) :
                           new StashRequest(() => Stash.Client.ExecuteAsync<string>(new RestRequest(url, HttpMethod.Delete)));
        }

        public override string Url
        {
            get { return Parent.Url + "/" + CommitId; }
        }
    }
}
