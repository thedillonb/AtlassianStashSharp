using System.Net.Http;
using AtlassianStashSharp.Extensions;
using AtlassianStashSharp.Models;
using PortableRest;

namespace AtlassianStashSharp.Controllers
{
    public class GroupsController : BaseController
    {
        internal GroupsController(StashClient stash) 
            : base(stash, null)
        {
        }

        public StashPaginatedRequest<string> GetAll()
        {
            return new StashPaginatedRequest<string>((start, limit) =>
                Stash.Client.ExecuteAsync<Pagination<string>>(
                    new RestRequest(Url, HttpMethod.Get).WithPagination(start, limit)));
        }

        public override string Url
        {
            get { return "/groups"; }
        }
    }
}
