using Microsoft.AspNetCore.Mvc;

namespace PersonelAPI1.Controllers
{
    public class AuthControllerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
