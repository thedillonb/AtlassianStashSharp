﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AtlassianStashSharp.Controllers;
using Newtonsoft.Json;

namespace AtlassianStashSharp
{
    public class StashClient
    {
        private readonly HttpClient _client;
        private readonly Uri _baseUri;
        private static JsonSerializerSettings SerializationSettings;

        public static Func<HttpClient> HttpClientFactory = () => new HttpClient(); 

        static StashClient()
        {
            SerializationSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        }

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

        public ProfileController Profile
        {
            get { return new ProfileController(this); }
        }

        public UsersController Users
        {
            get { return new UsersController(this); }
        }

        public BuildStatusController BuildStatus
        {
            get { return new BuildStatusController(this); }
        }

        public BranchUtilsController BranchUtilities
        {
            get { return new BranchUtilsController(this); }
        }

        public TimeSpan Timeout
        {
            get { return _client.Timeout; }
            set { _client.Timeout = value; }
        }

        private StashClient(Uri baseUrl)
        {
            _client = HttpClientFactory();
            _client.Timeout = new TimeSpan(0, 0, 25);
            _baseUri = baseUrl;
            _client.DefaultRequestHeaders.Add("User-Agent", "Atlassian Stash Sharp Client");
        }

        private static string CreateQueryUrl(string relativeUrl, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (parameters == null)
                return relativeUrl;

            var sb = new StringBuilder();
            foreach (var parameter in parameters.Where(x => x.Value != null))
            {
                sb.AppendFormat("&{0}={1}", Uri.EscapeUriString(parameter.Key),
                    Uri.EscapeDataString(parameter.Value.ToString()));
            }

            return string.Format(relativeUrl.Contains("?") ? "{0}{1}" : "{0}?{1}", relativeUrl, sb);
        }

        private HttpRequestMessage CreateRequestMessage(string relativeUrl, HttpMethod method,
                                                        IEnumerable<KeyValuePair<string, object>> parameters,
                                                        IEnumerable<KeyValuePair<string, object>> headers)
        {
            var baseUrl = _baseUri.AbsoluteUri;
            var queryUrl = CreateQueryUrl(relativeUrl, parameters);
            var uri = new Uri(baseUrl.TrimEnd('/') + "/" + queryUrl.TrimStart('/'));
            var request = new HttpRequestMessage(method, uri);

            if (headers != null)
            {
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value.ToString());
            }

            return request;
        }

        internal async Task Delete(string relativeUrl, IDictionary<string, object> parameters = null,
                                   IDictionary<string, object> headers = null,
                                   CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = CreateRequestMessage(relativeUrl, HttpMethod.Delete, parameters, headers);
            var response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        internal async Task<T> Post<T>(string relativeUrl, object postObject,
                                       IDictionary<string, object> parameters = null,
                                       IDictionary<string, object> headers = null,
                                       CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            var request = CreateRequestMessage(relativeUrl, HttpMethod.Post, parameters, headers);

            if (postObject is string)
            {
                request.Content = new StringContent(postObject.ToString(), Encoding.UTF8);
            }
            else if (postObject != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(postObject), Encoding.UTF8);
            }

            var response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await GetResponseContent<T>(response).ConfigureAwait(false);
        }

        internal async Task<T> Get<T>(string relativeUrl, IDictionary<string, object> parameters = null, 
                                      IDictionary<string, object> headers = null,
                                      CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            var request = CreateRequestMessage(relativeUrl, HttpMethod.Get, parameters, headers);
            var response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await GetResponseContent<T>(response).ConfigureAwait(false);
        }


        private static async Task<T> GetResponseContent<T>(HttpResponseMessage httpResponseMessage) where T : class
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
                return null;
   
            var rawResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (rawResponseContent == null) 
                return null;

            if (typeof(T) == typeof(string))
                return rawResponseContent as T;

            return JsonConvert.DeserializeObject<T>(rawResponseContent, SerializationSettings);
        }

        public static StashClient CrateBasic(Uri baseUri, string username, string password)
        {
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password)));
            var authHeader = string.Format("Basic {0}", token);
            var client = new StashClient(baseUri);
            client._client.DefaultRequestHeaders.Add("Authorization", authHeader);
            return client;
        }
    }
}
