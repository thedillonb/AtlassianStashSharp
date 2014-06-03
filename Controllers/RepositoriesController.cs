using System.Collections.Generic;
using AtlassianStashSharp.Models;

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
            return new StashPaginatedRequest<Repository>((start, limit, token) =>
                Stash.Get<Pagination<Repository>>(Url, new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit}
                }, cancellationToken: token));
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
            return new StashPaginatedRequest<Repository>((start, limit, token) =>
                Stash.Get<Pagination<Repository>>(Url, new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit},
                    {"name", name},
                    {"projectname", projectname},
                    {"permission", permission},
                    {"visibility", visibility}
                }, cancellationToken: token));
        }

        public override string Url
        {
            get { return "/rest/api/1.0/repos"; }
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
            return new StashRequest<Repository>(token => 
                Stash.Get<Repository>(Url, cancellationToken: token));
        }

        public StashPaginatedRequest<Repository> GetForks()
        {
            return new StashPaginatedRequest<Repository>((start, limit, token) =>
                Stash.Get<Pagination<Repository>>(Url + "/forks", new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit}
                }, cancellationToken: token));
        }

        public StashPaginatedRequest<Repository> GetRelated()
        {
            return new StashPaginatedRequest<Repository>((start, limit, token) =>
                Stash.Get<Pagination<Repository>>(Url + "/related", new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit}
                }, cancellationToken: token));
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

            return new StashRequest<ContentContainer>(token =>
                Stash.Get<ContentContainer>(url, new Dictionary<string, object>
                {
                    {"at", at},
                    {"type", type},
                    {"blame", blame},
                    {"noContent", noContent}
                }, cancellationToken: token));
        }

        public StashRequest<BrowseContent> GetFileContent(string path = null, string at = null, bool? type = null, string blame = null, string noContent = null)
        {
            var url = Url + "/browse";
            if (!string.IsNullOrEmpty(path))
            {
                if (path.StartsWith("/", System.StringComparison.Ordinal))
                    url += path;
                else
                    url += "/" + path;
            }

            return new StashRequest<BrowseContent>(token =>
                Stash.Get<BrowseContent>(url, new Dictionary<string, object>
                {
                    {"at", at},
                    {"type", type},
                    {"blame", blame},
                    {"noContent", noContent}
                }, cancellationToken: token));
        }

        public StashPaginatedRequest<Change> GetAllChanges(string since = null, string until = null)
        {
            return new StashPaginatedRequest<Change>((start, limit, token) =>
                Stash.Get<Pagination<Change>>(Url + "/changes", new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit},
                    {"since", since},
                    {"until", until}
                }, cancellationToken: token));
        }

        public override string Url
        {
            get { return Parent.Url + "/" + Slug; }
        }
    }

}
