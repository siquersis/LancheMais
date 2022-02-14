using LancheMais.WebApp.Entities;

namespace LancheMais.WebApp.Interfaces
{
    public interface ICategoriaRepository : IDisposable
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
