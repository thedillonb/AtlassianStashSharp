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
                if (name != null) req.AddQueryString("name", name);
                if (projectname != null) req.AddQueryString("projectname", projectname);
                if (permission != null) req.AddQueryString("permission", permission);
                if (visibility != null) req.AddQueryString("visibility", visibility);
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

        public StashRequest<ContentContainer> GetContents(string path = null, string at = null, bool? type = null, string blame = null, string noContent = null)
        {
            var url = Url + "/browse";
            if (!string.IsNullOrEmpty(path))
            {
                if (path.StartsWith("/", System.StringComparison.Ordinal))
                    url += path;
                else
                    url += "/" + path;
            }

            return new StashRequest<ContentContainer>(() =>
            {
                var req = new RestRequest(url, HttpMethod.Get);
                if (at != null) req.AddQueryString("at", at);
                if (type != null) req.AddQueryString("type", type.Value);
                if (blame != null) req.AddQueryString("blame", blame);
                if (noContent != null) req.AddQueryString("noContent", noContent);
                return Stash.Client.ExecuteAsync<ContentContainer>(req);
            });
        }

        public StashPaginatedRequest<Change> GetAllChanges(string since = null, string until = null)
        {
            return new StashPaginatedRequest<Change>((start, limit) =>
            {
                var req = new RestRequest(Url + "/changes", HttpMethod.Get).WithPagination(start, limit);
                if (since != null) req.AddQueryString("since", since);
                if (until != null) req.AddQueryString("until", until);
                return Stash.Client.ExecuteAsync<Pagination<Change>>(req);
            });
        }

        public override string Url
        {
            get { return Parent.Url + "/" + Slug; }
        }
    }

}
