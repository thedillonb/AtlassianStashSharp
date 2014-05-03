using System.Net.Http;
using AtlassianStashSharp.Models;
using PortableRest;

namespace AtlassianStashSharp.Controllers
{
    public class MarkupController : BaseController
    {
        internal MarkupController(StashClient stash) 
            : base(stash, null)
        {
        }

        public StashRequest<Markup> Preview(string markup)
        {
            return new StashRequest<Markup>(() =>
            {
                var req = new RestRequest(Url + "/preview", HttpMethod.Post);
                req.AddParameter(markup);
                return Stash.Client.ExecuteAsync<Markup>(req);
            });
        }

        public override string Url
        {
            get { return "/markup"; }
        }
    }
}
