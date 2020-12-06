using Aplicacao.Domain.Shared.Model;
using System.Collections.Generic;

namespace Aplicacao.Domain.Model
{
    public class Order : TEntity<int>
    {
        //public Order(Customer customer, ICollection<OrderItems> orderItems)
        //{
        //    Customer = new Customer(customer.Name);
        //    OrderItems = orderItems;
        //}

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
