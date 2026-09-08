using Microsoft.AspNetCore.Mvc;

namespace ConsultasUVV.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
