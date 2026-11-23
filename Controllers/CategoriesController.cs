using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniELearning.Data;
using MiniELearning.Models;

namespace MiniELearning.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _db.Categories
                .Include(c => c.Courses)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return View(categories);
        }

        // GET: /Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            category.Name = category.Name?.Trim() ?? string.Empty;

            if (ModelState.IsValid && await NameExistsAsync(category.Name))
            {
                ModelState.AddModelError(nameof(Category.Name), "Există deja o categorie cu acest nume.");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _db.Categories.Add(category);

            if (!await TrySaveAsync())
            {
                return View(category);
            }

            TempData["SuccessMessage"] = $"Categoria „{category.Name}” a fost adăugată.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: /Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            category.Name = category.Name?.Trim() ?? string.Empty;

            if (ModelState.IsValid && await NameExistsAsync(category.Name, excludeId: id))
            {
                ModelState.AddModelError(nameof(Category.Name), "Există deja o categorie cu acest nume.");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var existing = await _db.Categories.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = category.Name;

            if (!await TrySaveAsync())
            {
                return View(category);
            }

            TempData["SuccessMessage"] = $"Categoria „{existing.Name}” a fost actualizată.";
            return RedirectToAction(nameof(Index));
        }

        // Verifică dacă mai există o categorie cu același nume (în afară de cea editată).
        private Task<bool> NameExistsAsync(string name, int excludeId = 0)
        {
            return _db.Categories.AnyAsync(c => c.Name == name && c.Id != excludeId);
        }

        // Plasă de siguranță: dacă două persoane salvează același nume în același moment,
        // indexul unic din baza de date refuză a doua salvare.
        private async Task<bool> TrySaveAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(nameof(Category.Name), "Există deja o categorie cu acest nume.");
                return false;
            }
        }
    }
}