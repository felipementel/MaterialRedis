namespace Aplicacao.Application.DTOs
{
    public class ProductDTO : BaseDTOEntity<int>
    {
        public string Descricao { get; set; }

        public float Peso { get; set; }

        public string SKU { get; set; }

        public decimal Preco { get; set; }
    }
}
