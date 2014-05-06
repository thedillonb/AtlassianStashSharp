using System.Net.Http;
using AtlassianStashSharp.Extensions;
using AtlassianStashSharp.Models;
using PortableRest;

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
            return new StashPaginatedRequest<Project>((start, limit) => 
                Stash.Client.ExecuteAsync<Pagination<Project>>(
                    new RestRequest(Url, HttpMethod.Get).WithPagination(start, limit)));
        }


        public override string Url
        {
            get { return "/projects"; }
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
            return new StashRequest<Project>(() =>
                Stash.Client.ExecuteAsync<Project>(new RestRequest(Url)));
        }


        public StashRequest<string> GetAvatar(string size = null)
        {
            return new StashRequest<string>(() =>
            {
                var req = new RestRequest(Url + "/avatar.png");
                if (size != null) req.AddQueryString("s", size);
                return Stash.Client.ExecuteAsync<string>(req);
            });
        }

        public override string Url
        {
            get { return Parent.Url + "/" + Name; }
        }
    }
}
