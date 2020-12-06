using Aplicacao.Test.Scenarios.Base.CRUD;

namespace Aplicacao.Test.Scenarios
{
    public class ProductsTest : Operations
    {
        public ProductsTest()
        {
            Body = new
            {
                Descricao = "Iphone 12",
                Peso = 13.1,
                SKU = "FAAAAAT",
                Preco = 299.99
            };
            Configure("Orders", "1");
        }
    }
}
