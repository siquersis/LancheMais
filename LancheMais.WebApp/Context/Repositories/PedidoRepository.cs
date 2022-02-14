using LancheMais.WebApp.Context.Interfaces;
using LancheMais.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LancheMais.WebApp.Context.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
        {
            _context = context;
            _carrinhoCompra = carrinhoCompra;
        }

        public Pedido GetPedidoById(int pedidoId)
        {
            var pedido = _context.Pedido.AsNoTracking()
                                        .Include(pd => pd.ItensPedido
                                        .Where(pd => pd.PedidoId == pedidoId))
                                        .FirstOrDefault(p => p.Id == pedidoId);

            return pedido;
        }

        public List<Pedido> GetPedidos()
        {
            return _context.Pedido.ToList();
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _context.Pedido.Add(pedido);
            _context.SaveChanges();

            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItens;

            foreach (var carrinhoItem in carrinhoCompraItens)
            {
                var pedidoDetail = new PedidoDetalhe()
                {
                    Quantidade = carrinhoItem.Quantidade,
                    LancheId = carrinhoItem.Lanche.Id,
                    PedidoId = pedido.Id,
                    Preco = carrinhoItem.Lanche.Preco
                };
                _context.PedidoDetalhe.Add(pedidoDetail);
            }
           
            _context.SaveChanges();
        }
    }
}
