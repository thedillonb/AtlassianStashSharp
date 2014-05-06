using System.Net.Http;
using AtlassianStashSharp.Extensions;
using AtlassianStashSharp.Models;
using PortableRest;

namespace AtlassianStashSharp.Controllers
{
    public class BranchesController : BaseController
    {
        public BranchesController(StashClient stash, BaseController parent) 
            : base(stash, parent)
        {
        }

        public StashRequest<Branch> GetDefault()
        {
            return new StashRequest<Branch>(() =>
                Stash.Client.ExecuteAsync<Branch>(
                    new RestRequest(Url + "/default", HttpMethod.Get)));
        }

        public StashPaginatedRequest<Branch> GetAll(string branch = null, bool? details = null, string filterText = null, string orderBy = null)
        {
            return new StashPaginatedRequest<Branch>((start, limit) =>
            {
                var req = new RestRequest(Url, HttpMethod.Get).WithPagination(start, limit);
                if (branch != null) req.AddQueryString("branch", branch);
                if (details != null) req.AddQueryString("details", details.Value);
                if (filterText != null) req.AddQueryString("filterText", filterText);
                if (orderBy != null) req.AddQueryString("orderBy", orderBy);
                return Stash.Client.ExecuteAsync<Pagination<Branch>>(req);
            });
        }

        public override string Url
        {
            get { return Parent.Url + "/branches"; }
        }
    }
}
