using LancheMais.WebApp.Context;
using Microsoft.EntityFrameworkCore;

namespace LancheMais.WebApp.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string IdCarrinhoCompra { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider service)
        {
            //Define uma sessão
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //Obtem um serviço do tipo do nosso Contexto
            var contexto = service.GetService<AppDbContext>();

            //Obtem ou gera o Id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //Adtribui o Id do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoId);

            //Retorna o carrinho com o contexto e o Id atribuído ou obtido
            return new CarrinhoCompra(contexto)
            {
                IdCarrinhoCompra = carrinhoId,
            };
        }

        public void AddLancheCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItem
                                             .AsNoTracking()
                                             .SingleOrDefault(
                                              x => x.Lanche.Id == lanche.Id &&
                                              x.CarrinhoCompraId == IdCarrinhoCompra);

            if (carrinhoCompraItem is null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = IdCarrinhoCompra,
                    Lanche = lanche,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItem.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public int RemoveLancheCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItem
                                            .AsNoTracking()
                                            .SingleOrDefault(
                                             x => x.Lanche.Id == lanche.Id &&
                                             x.CarrinhoCompraId == IdCarrinhoCompra);

            var quantidade = 0;

            if (carrinhoCompraItem is not null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidade = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItem.Remove(carrinhoCompraItem);
                }
            }

            _context.SaveChanges();
            return quantidade;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItens ??=
                   _context.CarrinhoCompraItem
                           .AsNoTracking()
                           .Where(c => c.CarrinhoCompraId == IdCarrinhoCompra)
                           .Include(s => s.Lanche)
                           .ToList();
        }

        public void LimparCarrinho()
        {
            var carrinho = _context.CarrinhoCompraItem
                                   .AsNoTracking()
                                   .Where(c => c.CarrinhoCompraId == IdCarrinhoCompra);

            _context.CarrinhoCompraItem.RemoveRange(carrinho);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItem
                                .AsNoTracking()
                                .Where(x => x.CarrinhoCompraId == IdCarrinhoCompra)
                                .Select(y => y.Lanche.Preco * y.Quantidade).Sum();

            return total;
        }
    }
}
