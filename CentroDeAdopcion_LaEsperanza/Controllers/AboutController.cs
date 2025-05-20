using Microsoft.AspNetCore.Mvc;

namespace CentroDeAdopcion_LaEsperanza.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
