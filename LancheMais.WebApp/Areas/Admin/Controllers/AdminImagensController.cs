using LancheMais.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LancheMais.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _config;
        private readonly IWebHostEnvironment _host;

        public AdminImagensController(IOptions<ConfigurationImagens> config,
                                      IWebHostEnvironment host)
        {
            _config = config.Value;
            _host = host;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files is null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }

            if (files.Count > 10)
            {
                ViewData["Erro"] = "Error: Quantidade de arquivos excedeu o limite";
                return View(ViewData);
            }

            long size = files.Sum(f => f.Length);

            var filePathsName = new List<string>();

            var filePath = Path.Combine(_host.WebRootPath,
                _config.NomePastaImagensProdutos);

            foreach (var formFile in files)
            {
                if (formFile.FileName.Contains(".jpg") || 
                    formFile.FileName.Contains(".gif") ||
                    formFile.FileName.Contains(".bmp") ||
                    formFile.FileName.Contains(".png"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                    filePathsName.Add(fileNameWithPath);

                    using var stream = new FileStream(fileNameWithPath, FileMode.Create);
                    await formFile.CopyToAsync(stream);
                }
            }

            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, " +
                                     $"com tamanho total de : {size} bytes";

            ViewBag.Arquivos = filePathsName;

            return View(ViewData);
        }

        public IActionResult GetImagens()
        {
            FileManagerModel model = new();

            var userImagesPath = Path.Combine(_host.WebRootPath,
                _config.NomePastaImagensProdutos);

            DirectoryInfo dir = new(userImagesPath);

            FileInfo[] files = dir.GetFiles();

            model.PathImagensProduto = _config.NomePastaImagensProdutos;

            if (files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
            }

            model.Files = files;

            return View(model);
        }

        public IActionResult Deletefile(string fname)
        {
            string _imagemDeleta = Path.Combine(_host.WebRootPath,
                _config.NomePastaImagensProdutos + "\\", fname);

            if ((System.IO.File.Exists(_imagemDeleta)))
            {
                System.IO.File.Delete(_imagemDeleta);

                ViewData["Deletado"] = $"Arquivo(s) {_imagemDeleta} deletado com sucesso";
            }

            return View("index");
        }
    }
}
