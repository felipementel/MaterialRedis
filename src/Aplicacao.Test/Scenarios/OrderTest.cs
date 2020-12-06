using Aplicacao.Test.Scenarios.Base.CRUD;

namespace Aplicacao.Test.Scenarios
{
    public class OrderTest : Operations
    {
        public OrderTest()
        {
            Body = new
            {
              ProductId = 2,
              Quantity = 1,
              Price = 12
            };
            Configure("Orders", "1");
        }
    }
}
