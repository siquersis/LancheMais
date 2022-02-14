using LancheMais.WebApp.Models;

namespace LancheMais.WebApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Lanche> LanchesPreferidos { get; set; }
    }
}
