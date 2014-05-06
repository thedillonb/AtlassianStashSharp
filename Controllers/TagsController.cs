using System.Net.Http;
using AtlassianStashSharp.Extensions;
using AtlassianStashSharp.Models;
using PortableRest;

namespace AtlassianStashSharp.Controllers
{
    public class TagsController : BaseController
    {
        public TagsController(StashClient stash, BaseController parent) 
            : base(stash, parent)
        {
        }

        public StashPaginatedRequest<Tag> GetAll(string filterText = null, string orderBy = null)
        {
            return new StashPaginatedRequest<Tag>((start, limit) =>
            {
                var req = new RestRequest(Url, HttpMethod.Get).WithPagination(start, limit);
                if (filterText != null) req.AddQueryString("filterText", filterText);
                if (orderBy != null) req.AddQueryString("orderBy", orderBy);
                return Stash.Client.ExecuteAsync<Pagination<Tag>>(req);
            });
        }

        public override string Url
        {
            get { return Parent.Url + "/tags"; }
        }
    }
}
