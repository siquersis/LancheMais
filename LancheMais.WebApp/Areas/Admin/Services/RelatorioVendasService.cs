using LancheMais.WebApp.Context;
using LancheMais.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LancheMais.WebApp.Areas.Admin.Services
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext _context;

        public RelatorioVendasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.Pedido select obj;
            if (minDate.HasValue)
            {
                result = result.AsNoTracking()
                               .Where(x => x.PedidoEnviado >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.AsNoTracking()
                               .Where(x => x.PedidoEnviado <= maxDate.Value);
            }

            return await result
                              .AsNoTracking()
                              .Include(x => x.ItensPedido)
                              .ThenInclude(z => z.Lanche)
                              .OrderByDescending(y => y.PedidoEnviado)
                              .ToListAsync();
        }
    }
}
