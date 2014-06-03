using System.Collections.Generic;
using AtlassianStashSharp.Models;

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
            return new StashPaginatedRequest<User>((start, limit, cancellationToken) => 
                Stash.Get<Pagination<User>>(Url, new Dictionary<string, object>
            {
                {"filter", filter},
                {"start", start},
                {"limit", limit}
            }, cancellationToken: cancellationToken));
        }

        public override string Url
        {
            get { return "/rest/api/1.0/users"; }
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
            return new StashRequest<User>(cancellationToken =>
                Stash.Get<User>(Url, cancellationToken: cancellationToken));
        }

        public override string Url
        {
            get { return Parent.Url + "/" + UserSlug; }
        }
    }
}
