using Aplication.Util;
using Finjan.Integracao.Dynamics.Tests.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace Aplicacao.Test.Fixtures
{
    public abstract class Context
    {
        public static TestingConfiguration Configuration { get; set; }

        protected Context()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "environment.json");
            string json = File.ReadAllText(path);
            Configuration = JsonConvert.DeserializeObject<TestingConfiguration>(json);
        }
    }
}
