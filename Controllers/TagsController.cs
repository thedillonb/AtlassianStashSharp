using System.Collections.Generic;
using AtlassianStashSharp.Models;

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
            return new StashPaginatedRequest<Tag>((start, limit, cancellationToken) => 
                Stash.Get<Pagination<Tag>>(Url, new Dictionary<string, object>
            {
                {"start", start},
                {"limit", limit},
                {"filterText", filterText},
                {"orderBy", orderBy}
            }, cancellationToken: cancellationToken));
        }

        public override string Url
        {
            get { return Parent.Url + "/tags"; }
        }
    }
}
