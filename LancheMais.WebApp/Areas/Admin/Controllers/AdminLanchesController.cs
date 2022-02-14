using LancheMais.WebApp.Context;
using LancheMais.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace LancheMais.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminLanchesController : Controller
    {
        private readonly AppDbContext _context;

        public AdminLanchesController(AppDbContext context)
        {
            _context = context;
        }

        //// GET: Admin/AdminLanches
        //public async Task<IActionResult> Index()
        //{
        //    var appDbContext = _context.Lanche.Include(l => l.Categoria);
        //    return View(await appDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var result = _context.Lanche.Include(l => l.Categoria).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                result = result.Where(p => p.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(result, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        // GET: Admin/AdminLanches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanche
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        // GET: Admin/AdminLanches/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nome");
            return View();
        }

        // POST: Admin/AdminLanches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DescricaoCurta,DescricaoLonga,Preco,ImagemUrl,ImagemThumbnailUrl,IsLanchePerfeito,EmEstoque,CategoriaId")] Lanche lanche)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lanche);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nome", lanche.CategoriaId);
            return View(lanche);
        }

        // GET: Admin/AdminLanches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanche.FindAsync(id);
            if (lanche == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nome", lanche.CategoriaId);
            return View(lanche);
        }

        // POST: Admin/AdminLanches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DescricaoCurta,DescricaoLonga,Preco,ImagemUrl,ImagemThumbnailUrl,IsLanchePerfeito,EmEstoque,CategoriaId")] Lanche lanche)
        {
            if (id != lanche.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lanche);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LancheExists(lanche.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Nome", lanche.CategoriaId);
            return View(lanche);
        }

        // GET: Admin/AdminLanches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanche
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        // POST: Admin/AdminLanches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lanche = await _context.Lanche.FindAsync(id);
            _context.Lanche.Remove(lanche);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LancheExists(int id)
        {
            return _context.Lanche.Any(e => e.Id == id);
        }
    }
}
