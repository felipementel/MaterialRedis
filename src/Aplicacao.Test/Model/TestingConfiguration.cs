namespace Finjan.Integracao.Dynamics.Tests.Model
{
    public class TestingConfiguration
    {
        public bool IsDesenv { get; set; }
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string APNETCORE_ENVIROMENT { get; set; }
        public int? Port { get; set; }
        public string Login { get; set; }
        public string AccessKey { get; set; }
        public string UrlToken { get; set; }
    }
}
