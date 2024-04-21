using Microsoft.AspNetCore.Mvc;
using QuizApp.Components.Pages;

namespace QuizApp.Controllers
{
   
    public class BuzzerController : Controller
    {
        public IActionResult Index()
        {
            string key = "MyCookie";
            string value = "AshProgHelp";
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append(key, value, cookieOptions);
            return View("Components/Pages/Buzzer");
        }
    }
}
