namespace LancheMais.WebApp.Models
{
    public class CarrinhoCompraItem
    {
        public int Id { get; set; }
        public Lanche Lanche { get; set; }
        public int Quantidade { get; set; }
        public string CarrinhoCompraId { get; set; }
    }
}
