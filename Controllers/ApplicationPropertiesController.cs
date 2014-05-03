using AtlassianStashSharp.Models;
using PortableRest;

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
            return new StashRequest<ApplicationProperties>(() =>
                Stash.Client.ExecuteAsync<ApplicationProperties>(new RestRequest(Url)));
        }

        public override string Url
        {
            get { return "/application-properties"; }
        }
    }
}
