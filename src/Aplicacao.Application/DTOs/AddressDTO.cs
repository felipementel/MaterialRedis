using System;

namespace Aplicacao.Application.DTOs
{
    public class AddressDTO : BaseDTO
    {
        public string Pais { get; set; }

        public string Estado { get; set; }

        public string Cidade { get; set; }

        public string Bairro { get; set; }

        public string Rua { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string CEP { get; set; }
    }
}