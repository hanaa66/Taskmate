using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace sep_project.Areas.admin.Controllers
{
    [Area("admin")]
    public class LanguageController : Controller
    {
        public IActionResult Change_Language(string culter, string returnurl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culter)),
                new CookieOptions { Expires = DateTime.UtcNow.AddDays(1) }
                );
            return LocalRedirect(returnurl ?? "/");
        }
        
    }
}
