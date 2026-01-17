using Microsoft.AspNetCore.Mvc;

namespace EdukateMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
