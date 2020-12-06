using Aplicacao.Test.Scenarios.Base.CRUD;
using System;

namespace Aplicacao.Test.Scenarios
{
    public class CustomerTest : Operations
    {
        public CustomerTest()
        {
            Body = new
            {
                NomeCompleto = $"TESTE TDD {RandTest}",
                AddressDTO = new dynamic[] {
                  new {
                      Pais = "Brasil",
                      Estado = "RJ",
                      Cidade = "Rio de Janeiro",
                      Bairro = "Leblon",
                      Rua = "Avenida Delfin Moreira",
                      Numero = "999",
                      Complemento = "apto 1",
                      Cep = "22478999"
                  },
                  new {
                      Pais = "Brasil",
                      Estado = "SP",
                      Cidade = "São Paulo",
                      Bairro = "Jadins",
                      Rua = "Rua Suiça",
                      Numero = "777",
                      Cep = "02489585"
                  }
                 },
                DataNascimento = DateTime.Parse("2020-06-14T00:00:00", System.Globalization.CultureInfo.InvariantCulture),
                DataCadastro = DateTime.Parse("2020-06-14T17:51:14.2886725-03:00", System.Globalization.CultureInfo.InvariantCulture),
                Email = "felipementel@hotmail.com"
            };
            Configure("Customers", "1");
        }
    }
}
