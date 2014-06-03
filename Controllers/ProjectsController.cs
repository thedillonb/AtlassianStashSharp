using System.Collections.Generic;
using AtlassianStashSharp.Models;

namespace AtlassianStashSharp.Controllers
{
    public class ProjectsController : BaseController
    {
        public ProjectController this[string id]
        {
            get { return new ProjectController(Stash, this, id); }
        }

        public ProjectsController(StashClient stash) 
            : base(stash, null)
        {
        }

        public StashPaginatedRequest<Project> GetAll()
        {
            return new StashPaginatedRequest<Project>((start, limit, token) =>
                Stash.Get<Pagination<Project>>(Url, new Dictionary<string, object>
                {
                    {"start", start},
                    {"limit", limit}
                }, cancellationToken: token));
        }


        public override string Url
        {
            get { return "/rest/api/1.0/projects"; }
        }
    }

    public class ProjectController : BaseController
    {
        public string Name { get; private set; }

        public RepositoriesController Repositories
        {
            get { return new RepositoriesController(Stash, this); }
        }

        public ProjectController(StashClient stash, BaseController parent, string name) 
            : base(stash, parent)
        {
            Name = name;
        }

        public StashRequest<Project> Get()
        {
            return new StashRequest<Project>(token =>
                Stash.Get<Project>(Url, cancellationToken: token));
        }


        public StashRequest<string> GetAvatar(string size = null)
        {
            return new StashRequest<string>(token =>
                Stash.Get<string>(Url + "/avatar.png", new Dictionary<string, object>
                {
                    {"s", size}
                }, cancellationToken: token));
        }

        public override string Url
        {
            get { return Parent.Url + "/" + Name; }
        }
    }
}
