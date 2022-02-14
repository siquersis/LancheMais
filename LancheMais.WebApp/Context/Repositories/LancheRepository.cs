using LancheMais.WebApp.Context;
using LancheMais.WebApp.Interfaces;
using LancheMais.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LancheMais.WebApp.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches
            => _context.Lanche.Include(x => x.Categoria);

        public IEnumerable<Lanche> LanchePreferido
            => _context.Lanche.Where(x => x.IsLanchePerfeito).Include(x => x.Categoria);

        public async Task<Lanche> GetLanche(int id)
            => await _context.Lanche.FirstOrDefaultAsync(x => x.Id == id);

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
