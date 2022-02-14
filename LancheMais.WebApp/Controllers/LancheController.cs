using LancheMais.WebApp.Interfaces;
using LancheMais.WebApp.Models;
using LancheMais.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LancheMais.WebApp.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(x => x.Id);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                //if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
                //{
                //    lanches = _lancheRepository.Lanches
                //                               .Where(x => x.Categoria.Nome.Equals("Normal"))
                //                               .OrderBy(x => x.Nome);
                //}
                //else
                //{
                //    lanches = _lancheRepository.Lanches
                //                               .Where(x => x.Categoria.Nome.Equals("Natural"))
                //                               .OrderBy(x => x.Nome);
                //}
                lanches = _lancheRepository.Lanches
                                           .Where(x => x.Categoria.Nome.Equals(categoria))
                                           .OrderBy(x => x.Nome);
                categoriaAtual = categoria;
            }
            var lanchesListVM = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesListVM);
        }

        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepository.Lanches
                                          .FirstOrDefault(x => x.Id == lancheId);

            return View(lanche);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categorialAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(x => x.Id);
                categorialAtual = "Todos os Lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches
                                           .Where(x => x.Nome.ToLower().Contains(searchString.ToLower()));

                if (lanches.Any())
                    categorialAtual = "Lanches";
                else
                    categorialAtual = "Nenhun lanche encontrado!";
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categorialAtual
            });

        }
    }
}
