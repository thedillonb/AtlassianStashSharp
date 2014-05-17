using AtlassianStashSharp.Models;

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
            return new StashRequest<Markup>(token => Stash.Post<Markup>(Url + "/preview", markup, cancellationToken: token));
        }

        public override string Url
        {
            get { return "/markup"; }
        }
    }
}
