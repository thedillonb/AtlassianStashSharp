using System.Net.Http;
using AtlassianStashSharp.Extensions;
using AtlassianStashSharp.Models;
using PortableRest;

namespace AtlassianStashSharp.Controllers
{
    public class RepositoriesController : BaseController
    {
        public RepositoryController this[string slug]
        {
            get { return new RepositoryController(Stash, this, slug); }
        }

        public RepositoriesController(StashClient stash, BaseController parent) 
            : base(stash, parent)
        {
        }

        public StashPaginatedRequest<Repository> GetAll()
        {
            return new StashPaginatedRequest<Repository>((start, limit) =>
                Stash.Client.ExecuteAsync<Pagination<Repository>>(
                    new RestRequest(Url, HttpMethod.Get).WithPagination(start, limit)));
        }

        public override string Url
        {
            get { return Parent.Url + "/repos"; }
        }
    }

    public class AllRepositoriesController : BaseController
    {
        public AllRepositoriesController(StashClient stash)
            : base(stash, null)
        {
        }

        public StashPaginatedRequest<Repository> GetAll(string name = null, string projectname = null, string permission = null, string visibility = null)
        {
            return new StashPaginatedRequest<Repository>((start, limit) =>
            {
                var req = new RestRequest(Url, HttpMethod.Get).WithPagination(start, limit);
                if (name != null) req.AddParameter("name", name);
                if (projectname != null) req.AddParameter("projectname", projectname);
                if (permission != null) req.AddParameter("permission", permission);
                if (visibility != null) req.AddParameter("visibility", visibility);
                return Stash.Client.ExecuteAsync<Pagination<Repository>>(req);
            });
        }

        public override string Url
        {
            get { return "/repos"; }
        }
    }

    public class RepositoryController : BaseController
    {
        public string Slug { get; private set; }

        public BranchesController Branches
        {
            get { return new BranchesController(Stash, this); }
        }

        public TagsController Tags
        {
            get { return new TagsController(Stash, this); }
        }

        public PullRequestsController PullRequests
        {
            get { return new PullRequestsController(Stash, this); }
        }

        public FilesController Files
        {
            get { return new FilesController(Stash, this); }
        }

        public CommitsController Commits
        {
            get { return new CommitsController(Stash, this); }
        }

        public RepositoryController(StashClient stash, BaseController parent, string slug)
            : base(stash, parent)
        {
            Slug = slug;
        }

        public StashRequest<Repository> Get()
        {
            return new StashRequest<Repository>(() =>
                Stash.Client.ExecuteAsync<Repository>(new RestRequest(Url, HttpMethod.Get)));
        }

        public StashPaginatedRequest<Repository> GetForks()
        {
            return new StashPaginatedRequest<Repository>((start, limit) =>
                Stash.Client.ExecuteAsync<Pagination<Repository>>(
                    new RestRequest(Url + "/forks", HttpMethod.Get).WithPagination(start, limit)));
        }

        public StashPaginatedRequest<Repository> GetRelated()
        {
            return new StashPaginatedRequest<Repository>((start, limit) =>
                Stash.Client.ExecuteAsync<Pagination<Repository>>(
                    new RestRequest(Url + "/related", HttpMethod.Get).WithPagination(start, limit)));
        }

        public StashPaginatedRequest<Change> GetAllChanges(string since = null, string until = null)
        {
            return new StashPaginatedRequest<Change>((start, limit) =>
            {
                var req = new RestRequest(Url + "/changes", HttpMethod.Get).WithPagination(start, limit);
                if (since != null) req.AddParameter("since", since);
                if (until != null) req.AddParameter("until", until);
                return Stash.Client.ExecuteAsync<Pagination<Change>>(req);
            });
        }

        public override string Url
        {
            get { return Parent.Url + "/" + Slug; }
        }
    }

}
