using System.Collections.Generic;

namespace Aplicacao.Application.DTOs
{
    public class OrderDTO : BaseDTOEntity<int>
    {
        public int CustomerId { get; set; }

        public List<OrderItemDTO> OrderItemsDTO { get; set; }
    }
}
