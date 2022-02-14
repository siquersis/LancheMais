using LancheMais.WebApp.Models;

namespace LancheMais.WebApp.Interfaces
{
    public interface ILancheRepository : IDisposable
    {
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchePreferido { get; }
        Task<Lanche> GetLanche(int id);
    }
}
