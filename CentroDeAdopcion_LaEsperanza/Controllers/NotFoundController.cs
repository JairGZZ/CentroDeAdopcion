using Microsoft.AspNetCore.Mvc;

namespace CentroDeAdopcion_LaEsperanza.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
