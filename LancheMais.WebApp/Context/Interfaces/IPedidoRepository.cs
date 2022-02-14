using LancheMais.WebApp.Models;

namespace LancheMais.WebApp.Context.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}
