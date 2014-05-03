using System.Net.Http;
using AtlassianStashSharp.Extensions;
using AtlassianStashSharp.Models;
using PortableRest;

namespace AtlassianStashSharp.Controllers
{
    public class UsersController : BaseController
    {
        public UserController this[string userSlug]
        {
            get { return new UserController(Stash, this, userSlug); }
        }

        public UsersController(StashClient stash) 
            : base(stash, null)
        {
        }

        public StashPaginatedRequest<User> GetAll(string filter = null)
        {
            return new StashPaginatedRequest<User>((start, limit) =>
            {
                var req = new RestRequest(Url, HttpMethod.Get).WithPagination(start, limit);
                if (filter != null) req.AddParameter("filter", filter);
                return Stash.Client.ExecuteAsync<Pagination<User>>(req);
            });
        }

        public override string Url
        {
            get { return "/users"; }
        }
    }

    public class UserController : BaseController
    {
        public string UserSlug { get; private set; }

        public RepositoriesController Repositories
        {
            get { return new RepositoriesController(Stash, this); }
        }

        public UserController(StashClient stash, BaseController parent, string userSlug)
            : base(stash, parent)
        {
            UserSlug = userSlug;
        }

        public StashRequest<User> Get()
        {
            return new StashRequest<User>(() => Stash.Client.ExecuteAsync<User>(new RestRequest(Url, HttpMethod.Get)));
        }

        public override string Url
        {
            get { return Parent.Url + "/" + UserSlug; }
        }
    }
}
