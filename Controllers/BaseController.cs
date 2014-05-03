namespace AtlassianStashSharp.Controllers
{
    public abstract class BaseController
    {
        protected StashClient Stash { get; private set; }

        protected BaseController Parent { get; private set; }

        protected BaseController(StashClient stash, BaseController parent)
        {
            Parent = parent;
            Stash = stash;
        }

        public abstract string Url { get; }
    }
}
