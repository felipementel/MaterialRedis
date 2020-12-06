using Microsoft.AspNetCore.Mvc;

namespace Aplicacao.Test.Model
{
    public class Retorno
    {
        public bool success { get; set; }
        public object data { get; set; }
        public int totalCount { get; set; }
        public ObjectResult Result { get; set; }
    }
}
