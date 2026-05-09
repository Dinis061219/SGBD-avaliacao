namespace AutuShopping.API.Models
{
    public class Produto
    {
        public int ProdutoID { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal PrecoVenda { get; set; }
        public int Estoque { get; set; }
        public int EstoqueMinimo { get; set; }
    }
}