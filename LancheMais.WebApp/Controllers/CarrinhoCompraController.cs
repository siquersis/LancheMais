using LancheMais.WebApp.Interfaces;
using LancheMais.WebApp.Models;
using LancheMais.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LancheMais.WebApp.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository,
                                        CarrinhoCompra carrinhoCompra
                                       )
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }

        [Authorize]
        public RedirectToActionResult AddItemNoCarrinho(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches
                                                     .FirstOrDefault(x => x.Id == lancheId);

            if (lancheSelecionado is not null)
            {
                _carrinhoCompra.AddLancheCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoverItemCarrinho(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches
                                                     .FirstOrDefault(x => x.Id == lancheId);

            if (lancheSelecionado is not null)
            {
                _carrinhoCompra.RemoveLancheCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }
    }
}