using Finjan.Integracao.Dynamics.Tests.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using Aplication.Util;
using Castle.Core.Internal;
using Aplicacao.Test.Fixtures;
using Aplicacao.Test.Model;

namespace Aplicacao.Test.Scenarios.Base
{
    public abstract class BaseTest
    {
        public bool IsSuccess { get; set; } = false;
        public string Controllers { get; set; }
        private string[] FieldOrderBy { get; set; } = new[] { "Id" };
        private string[] Id { get; set; } = new[] { "0" };
        private string Method { get; set; } = string.Empty;
        private string[] ParamMethod { get; set; } = new[] { "0" };
        public dynamic Body { get; set; }
        protected string RandTest { get { return new Random().Next(0, 10000).ToString(); } }
        readonly Func<string, string> f = s => string.IsNullOrEmpty(s) ? string.Empty : string.Format("/{0}", s);
        private HttpRequestMessage Request(HttpMethod method, string Uri = null) =>
            new HttpRequestMessage(method, $"{Uri ?? Url}") { Content = new StringContent(JsonConvert.SerializeObject(Body), Encoding.UTF8, "application/json") };

        public void Configure(string controllers, string id = null, string fieldOrderBy = null, string method = null, string paramMethod = null)
        {
            Controllers = controllers;
            Id = id.AsParameters();
            if (fieldOrderBy != null) FieldOrderBy = fieldOrderBy.AsParameters();
            Method = method ?? Method;
            ParamMethod = paramMethod.AsParameters();
        }

        public string UrlGetAll => String.Format("{0}/GetAll/{1}/asc/50/0", Url, FieldOrderBy.GetPath());

        private string GetPath() => !(Method.GetPath().IsNullOrEmpty() && ParamMethod.GetPath().IsNullOrEmpty()) ? string.Concat(Method.GetPath(), ParamMethod.GetPath()) : Id.GetPath();

        public string UrlGet => String.Format("{0}/{1}", Url, GetPath());

        public string Url => String.Format("{0}/{1}/v1", f(apiContext.Path), Controllers);
        public string GetUrl(string url) => !string.IsNullOrEmpty(url) ? string.Concat(Url, '/', url) : null
;

        public ApiContext apiContext => ApiContext.Instance;

        public static HttpClient Client;

        protected BaseTest()
        {
            Client = apiContext.Client;
            PrepareToken();
        }

        internal void PrepareToken()
        {
            Body = new { login = apiContext.Login, accessKey = apiContext.AccesKey };

            var urlToken = Request(HttpMethod.Post, apiContext.UrlToken) ;

            var response = Client.SendAsync(urlToken).Result;
            IsSuccess = response.IsSuccessStatusCode;
            if (IsSuccess)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var token = JsonConvert.DeserializeObject<Token>(result);

                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.accessToken);
            }
        }

        internal void GetAll(string url = null)
        {
            SetDataAsync(Client.GetAsync(GetUrl(url) ?? UrlGetAll).Result);
        }

        internal void Get(string url = null)
        {
            SetDataAsync(Client.GetAsync(GetUrl(url) ?? UrlGet).Result);
        }

        internal void SendTest(HttpMethod method)
        {
            var response = Client.SendAsync(Request(method)).Result;

            IsSuccess = response.IsSuccessStatusCode;
            if (IsSuccess)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                Body = JsonConvert.DeserializeObject<Retorno>(result).data;
            }        
        }

        internal void SetDataAsync(HttpResponseMessage message)
        {
            IsSuccess = message.IsSuccessStatusCode && !message.StatusCode.Equals(HttpStatusCode.NoContent);
            if (IsSuccess)
            {
                var result = message.Content.ReadAsStringAsync().Result;
                Body = JsonConvert.DeserializeObject<Retorno>(result).data;
            }
        }

        internal void Post() => SendTest(HttpMethod.Post);

        internal void Put() => SendTest(HttpMethod.Put);

        internal void Delete() => SendTest(HttpMethod.Delete);
    }
}