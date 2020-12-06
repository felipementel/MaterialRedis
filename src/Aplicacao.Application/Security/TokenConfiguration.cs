using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.Application.Security
{
    public class TokenConfiguration
    {
        public string  Audience { get; set; }

        public string Issuer{ get; set; }

        public int Seconds { get; set; }
    }
}
