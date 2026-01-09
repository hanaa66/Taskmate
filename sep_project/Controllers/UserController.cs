using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sep_project.Models;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;

namespace sep_project.Controllers
{
    public class UserController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
       public UserController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }
        [AllowAnonymous]
        public IActionResult Regester()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> index()
        {
            var model = await _userManager.Users.ToListAsync();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Regester(UserAccount model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    Email = model.User_Email,
                    UserName = model.User_Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                    return RedirectToAction("Index", "Home");
                }
            }
                return View(model);
            
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginAccount model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: true);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                   
                }
            }
            return View(model);
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }
       
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create_Role()
        {
            return View();
        }
      
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Role(string Role_Name)
        {
            if(string.IsNullOrWhiteSpace(Role_Name))
            {
                ViewBag.err_msg = "Enter Valid Role Name";
                return View();
            }
            else
            {
                var exist = await _roleManager.RoleExistsAsync(Role_Name);
                if(exist)
                {
                    ViewBag.err_msg= "The Role Already Exist";
                }
                else
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(Role_Name));
                    ViewBag.err_msg = "added";
                }
                
            }
            return View();
        }
       
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult>Edit(string id)
        {
            var user=await _userManager.FindByIdAsync(id);
            if(user==null) { return NotFound(); }
            var model = new UserRolesViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Roles = new List<RoleViewModel>()
            };
            var roleslist =await _roleManager.Roles.ToListAsync();
            foreach (var role in roleslist)
            {
                var isinrole = await _userManager.IsInRoleAsync(user, role.Name);
                model.Roles.Add(new RoleViewModel
                {
                    RoleName = role.Name,
                    IsSelected = isinrole
                });
            }
                return View(model);
            }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult>Edit(UserRolesViewModel model)
        {
            var user= await _userManager.FindByIdAsync(model.UserId);
            if(user==null) { return NotFound(); };
            var userroles = await _userManager.GetRolesAsync(user);
            var select_role=model.Roles.Where(e=>e.IsSelected).Select(equals=>equals.RoleName).ToList();
            await _userManager.RemoveFromRolesAsync(user, userroles);
            await _userManager.AddToRolesAsync(user, select_role);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete_User(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
               
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return RedirectToAction("Index"); 
            }

            return RedirectToAction("Index");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
