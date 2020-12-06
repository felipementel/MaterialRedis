using Aplicacao.API;
using Aplicacao.Test.Fixtures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Aplicacao.Test.Fixtures
{
    public class ApiContext : Context
    {
        private static ApiContext instance = null;

        private ApiContext() { }

        public static ApiContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApiContext();
                    instance.ClientConfig();
                }
                return instance;
            }
        }

        public HttpClient Client { get; set; }

        public bool Isdesenv { get; set; }

        public string Login => Configuration.Login;

        public string AccesKey => Configuration.AccessKey;

        public string UrlToken => Configuration.UrlToken;

        public string Path => Configuration.Path;

        public string Host => Configuration.Host;

        private void ClientConfig()
        {
            instance.Isdesenv = Configuration.IsDesenv;
            if (instance.Isdesenv)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false)
                    .AddEnvironmentVariables();

                var webBuilder = new WebHostBuilder()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseConfiguration(builder.Build())
                    .UseEnvironment(Configuration.APNETCORE_ENVIROMENT)
                    .UseStartup<Startup>();

                var server = new TestServer(webBuilder) 
                { 
                    BaseAddress = new Uri(Host)
                };

                server.Host.Start();

                instance.Client = server.CreateClient();

                instance.Client
                     .BaseAddress = new Uri(Host);
            }
            else
            {
                instance.Client = new HttpClient()
                {
                    BaseAddress = new Uri(Host)
                };
            }

            instance.Client
                .DefaultRequestHeaders
                .Accept.Clear();

            instance.Client
                .DefaultRequestHeaders
                .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
       
    }
}
