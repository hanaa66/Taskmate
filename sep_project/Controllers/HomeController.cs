using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sep_project.Models;

namespace sep_project.Controllers
{
    public class HomeController : Controller
    {
      



        public IActionResult Index()
        {
           
            return View("index");
        }
        
    }
}
