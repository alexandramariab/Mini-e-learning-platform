using Microsoft.AspNetCore.Mvc;
using MiniELearning.Models;
using MiniELearning.Data;
using Microsoft.AspNetCore.Mvc.Rendering; // SelectListItem (lista pentru dropdown)
using Microsoft.EntityFrameworkCore; // Include(), ToListAsync(), FirstOrDefaultAsync() etc.

namespace MiniELearning.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext db;

        public CoursesController(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var query = db.Courses.Include(c => c.Category)
                                  .OrderBy(c => c.Title);
            var courses = await query.ToListAsync();

            ViewBag.Courses = courses;
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.Message = TempData["SuccessMessage"];
            }

            return View();
        }

        public async Task<IActionResult> Show(int id)
        {
            Course? c = await db.Courses
                                .Include(e => e.Category)
                                .FirstOrDefaultAsync();
            if (c is null)
            {
                return NotFound();
            }

            return View(c);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Course c = new Course();

            c.CategoryList = await GetAllCategories();

            return View(c);

        }

        [HttpPost]
        public async Task<IActionResult> Create(Course c)
        {
            try
            {
                db.Courses.Add(c);
                await db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Course added succesfully.";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                c.CategoryList = await GetAllCategories();
                return View(c);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            Course? c = await db.Courses
                                .Include(e => e.Category)
                                .FirstOrDefaultAsync(e => e.Id == id);
            if(c is null)
            {
                return NotFound();
            }
            c.CategoryList = await GetAllCategories();

            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Course requestCourse)
        {
            Course? c = await db.Courses
                                .Include(e => e.Category)
                                .FirstOrDefaultAsync(e => e.Id == id);
            if (c is null)
            {
                return NotFound();
            }
            try
            {
                c.Title = requestCourse.Title;
                c.Description = requestCourse.Description;
                c.MaxEnrollments = requestCourse.MaxEnrollments;
                c.Level = requestCourse.Level;
                c.Status = requestCourse.Status;
                c.AdminFeedback = requestCourse.AdminFeedback;
                c.CategoryId = requestCourse.CategoryId;

                await db.SaveChangesAsync();
                TempData["SuccessMessage"] = "The course was edited succesfully.";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                requestCourse.CategoryList = await GetAllCategories();
                return View(requestCourse);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            Course? c = await db.Courses.FindAsync(id);

            if (c is null)
            {
                return NotFound();
            }

            db.Courses.Remove(c);
            await db.SaveChangesAsync();
            TempData["SuccessMessage"] = "Course deleted succesfully.";
            return RedirectToAction("Index");
        }

        [NonAction]
        public async Task<IEnumerable<SelectListItem>> GetAllCategories()
        {
            var selectList = new List<SelectListItem>();

            var categories = await (from cat in db.Categories
                                    orderby cat.Name
                                    select cat).ToListAsync();
            foreach (var category in categories)
            {
                selectList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }
            return selectList;
        }
    }
}
