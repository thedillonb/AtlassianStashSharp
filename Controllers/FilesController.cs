using System.Net.Http;
using AtlassianStashSharp.Extensions;
using AtlassianStashSharp.Models;
using PortableRest;

namespace AtlassianStashSharp.Controllers
{
    public class FilesController : BaseController
    {
        public FilesController(StashClient stash, BaseController parent) 
            : base(stash, parent)
        {
        }

        public StashPaginatedRequest<string> GetAll(string path = null, string at = null)
        {
            var url = Url;
            if (!string.IsNullOrEmpty(path))
                url = url + "/" + path;

            return new StashPaginatedRequest<string>((start, limit) =>
            {
                var req = new RestRequest(url, HttpMethod.Get).WithPagination(start, limit);
                if (at != null) req.AddQueryString("at", at);
                return Stash.Client.ExecuteAsync<Pagination<string>>(req);
            });
        }

        public override string Url
        {
            get { return Parent.Url + "/files"; }
        }
    }
}
