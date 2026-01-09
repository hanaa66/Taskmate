using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sep_project.Models;
using System.Diagnostics;
using System.Threading.Tasks;


namespace sep_project.Areas.admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(
            AppDbContext context,
            ILogger<HomeController> logger,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

    




        public IActionResult Index()
        {
            var data = _context.Employees.Include(e => e.department).ToList();
            return View("index", data);
        }
        [Authorize(Roles = "user")]
        public async Task<IActionResult>task()
        {
            var userId = _userManager.GetUserId(User);

       
            var tasks = await _context.Tasks
                        .Where(t => t.UserId == userId)
                        .ToListAsync();

            return View(tasks);
        }
  

        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Create_Task()
        {
            return View();
        }

        
        [HttpPost]
        [Authorize(Roles = "user")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Task(sep_project.Models.Task model)
        
        {
            if (ModelState.IsValid)
            {
                model.UserId = _userManager.GetUserId(User);
                _context.Tasks.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("task");
            }

            return View(model);
        }


       
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Edit_Task(int id)
        {
            var userId = _userManager.GetUserId(User);

            var task = await _context.Tasks
                       .FirstOrDefaultAsync(t => t.Task_Id == id && t.UserId == userId);

            if (task == null)
                return Unauthorized();

            return View(task);
        }


        [HttpPost]
        [Authorize(Roles = "user")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Task(sep_project.Models.Task model,int id)
        {
            var userId = _userManager.GetUserId(User);

            var task = await _context.Tasks
                       .FirstOrDefaultAsync(t => t.Task_Id == id && t.UserId == userId);

            if (task == null)
                return Unauthorized();

      
            task.Title = model.Title;
            task.Description =model.Description;
            task.Deadline = model.Deadline;

            await _context.SaveChangesAsync();

            return RedirectToAction("Task");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> ToggleCompleted(int id)
        {
            var userId = _userManager.GetUserId(User);

            var task = await _context.Tasks
                        .FirstOrDefaultAsync(t => t.Task_Id == id && t.UserId == userId);

            if (task == null)
                return Unauthorized();

            task.IsCompleted = !task.IsCompleted; 
            await _context.SaveChangesAsync();

            return RedirectToAction("Task"); 
        }



        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task <IActionResult> Delete_Task(int id)
        {
            var userId = _userManager.GetUserId(User);

            var task = await _context.Tasks
                        .FirstOrDefaultAsync(t => t.Task_Id == id && t.UserId == userId);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete_Task")]
        [Authorize(Roles = "user")]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);

            var task = await _context.Tasks
                        .FirstOrDefaultAsync(t => t.Task_Id == id && t.UserId == userId);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return RedirectToAction("Task");
        }
        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.depr = new SelectList(_context.Departments, "Department_Id", "Department_Name");
            return View("Create");
        }
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                model.Department_Id = 1;
                model.Hire_Date = DateTime.Now;
                //model.Image_Name =UploadFile(File, "images");
                _context.Employees.Add(model);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            ViewBag.depr = new SelectList(_context.Departments, "Department_Id", "Department_Name");
            return View("Create", model);
        }
       
        //public string UploadFile(List<IFormFile> File, string FolderName)
        //{
        //    var allowedextention = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        //    foreach (var file in File)
        //    {
        //        if (file.Length > 0)
        //        {

        //            var extention = Path.GetExtension(file.FileName).ToLower();
        //            if (!allowedextention.Contains(extention))
        //            {
        //                throw new InvalidOperationException("invalid extention");
        //            }
        //            string imagename = $"{Guid.NewGuid()}{DateTime.Now:yyyy}{extention}";
        //            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", "FolderName");
        //            var filepath = Path.Combine(folderpath, imagename);

        //            using (var stream = System.IO.File.Create(filepath))
        //            {
        //                file.CopyTo(stream);
        //            }
        //            return imagename;
        //        }
        //    }
        //        return string.Empty;
        //    }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Delete(int id)
        {
            var data = _context.Employees.Include(e => e.department).FirstOrDefault(x => x.Employee_Id == id);
            if (data == null)
            {
                return NotFound();
            }
            return View("Delete", data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmedd(int id)
        {
            var data = _context.Employees.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();

            }
            ViewBag.Department_Id = new SelectList(_context.Departments, "Department_Id", "Department_Name");
            return View("Edit", emp);
        }
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                var emp_data = _context.Employees.FirstOrDefault(x => x.Employee_Id == model.Employee_Id);
                if (emp_data == null)
                { return NotFound(); }
                emp_data.Employee_Name = model.Employee_Name;
                emp_data.Employee_Country = model.Employee_Country;
                emp_data.Employee_Email = model.Employee_Email;
                emp_data.Employee_Salary = model.Employee_Salary;
                emp_data.Hire_Date = DateTime.Now;
                emp_data.Department_Id = model.Department_Id;
                _context.SaveChanges();
                return RedirectToAction("index");

            }

            ViewBag.Department_Id = new SelectList(_context.Departments, "Department_Id", "Department_Name");

            return View("Edit", model);
        }



    }
}
