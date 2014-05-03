using System;
using System.Text;
using AtlassianStashSharp.Controllers;
using Newtonsoft.Json;
using PortableRest;

namespace AtlassianStashSharp
{
    public class StashClient
    {
        internal readonly RestClient Client;

        public ProjectsController Projects
        {
            get { return new ProjectsController(this); }
        }

        public ApplicationPropertiesController ApplicationProperties
        {
            get { return new ApplicationPropertiesController(this); }
        }

        public MarkupController Markup
        {
            get { return new MarkupController(this); }
        }

        public GroupsController Groups
        {
            get { return new GroupsController(this); }
        }

        public AllRepositoriesController Repositories
        {
            get { return new AllRepositoriesController(this); }
        }

        private StashClient(string baseUrl)
        {
            Client = new RestClient {BaseUrl = baseUrl};
        }

        public static StashClient CrateBasic(string baseUrl, string username, string password)
        {
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password)));
            var authHeader = string.Format("Basic {0}", token);
            var client = new StashClient(baseUrl);
            client.Client.AddHeader("Authorization", authHeader);
            return client;
        }
    }
}
