using Microsoft.AspNetCore.Mvc;
using sep_project.Models;
using System.Threading.Tasks;

namespace sep_project.Areas.admin.Controllers
{
    [Area("admin")]
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;
        public DepartmentController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var data = _context.Departments.ToList();
            return View(data);
        }
        [HttpGet]

        public IActionResult Create()
        {
            return View("create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department model)
        {
            if (!ModelState.IsValid)
            {
                TempData["showError"] = "make sure all sections added";
                return View("index", model);
            }
            await _context.Departments.AddAsync(model);
            await _context.SaveChangesAsync();
            TempData["showSuccess"]= "True";
            return RedirectToAction("index");


        }
    }
}

