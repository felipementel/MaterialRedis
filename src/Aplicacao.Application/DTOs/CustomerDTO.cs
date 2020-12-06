using System;
using System.Collections.Generic;

namespace Aplicacao.Application.DTOs
{
    public class CustomerDTO : BaseDTOEntity<int>
    {
        public string NomeCompleto { get; set; }

        public List<AddressDTO> addressDTO { get; set; }

        public DateTime DataNascimento { get; set; }

        public DateTime DataCadastro { get; set; }

        public string Email { get; set; }

        //public string Documento { get; set; }
    }
}