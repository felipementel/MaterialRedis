using System.Collections.Generic;

namespace Aplicacao.Application.DTOs
{
    public class OrderItemDTO : BaseDTOEntity<int>
    {
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
