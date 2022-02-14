using LancheMais.WebApp.Models;

namespace LancheMais.WebApp.ViewModels
{
    public class PedidoLancheViewModel
    {
        public Pedido Pedido { get; set; }
        public IEnumerable<PedidoDetalhe> Detalhes { get; set; }
    }
}
