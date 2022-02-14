using LancheMais.WebApp.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace LancheMais.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminGraficosController : Controller
    {
        private readonly GraficoVendasService _graficoVendas;

        public AdminGraficosController(GraficoVendasService graficoVendas)
        {
            _graficoVendas = graficoVendas ?? throw
                new ArgumentNullException(nameof(graficoVendas));
        }

        public JsonResult VendasLanches(int dias)
        {
            var lanchesVendasTotais = _graficoVendas.GetVendasLanches(dias);
            return Json(lanchesVendasTotais);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendasMensal()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendasSemanal()
        {
            return View();
        }
    }
}
