using Aplicacao.Domain.Shared.Model;

namespace Aplicacao.Domain.Model
{
    public class OrderItems : TEntity<int>
    {
        //public OrderItems(int productId, Product product, Order order, int orderId, int quantity, decimal price)
        //{
        //    ProductId = productId;
        //    Product = product;
        //    Order = order;
        //    OrderId = orderId;
        //    Quantity = quantity;
        //    Price = price;
        //}

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
