using System.Collections.Generic;
using AtlassianStashSharp.Models;

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

            return new StashPaginatedRequest<string>((start, limit, token) => 
                Stash.Get<Pagination<string>>(url, new Dictionary<string, object>
            {
                {"start", start},
                {"limit", limit},
                {"at", at}
            }, cancellationToken: token));
        }

        public override string Url
        {
            get { return Parent.Url + "/files"; }
        }
    }
}
