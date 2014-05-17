using AtlassianStashSharp.Models;

namespace AtlassianStashSharp.Controllers
{
    public class ApplicationPropertiesController : BaseController
    {
        internal ApplicationPropertiesController(StashClient stash) 
            : base(stash, null)
        {
        }

        public StashRequest<ApplicationProperties> Get()
        {
            return new StashRequest<ApplicationProperties>(token => Stash.Get<ApplicationProperties>(Url, cancellationToken: token));
        }

        public override string Url
        {
            get { return "/application-properties"; }
        }
    }
}
