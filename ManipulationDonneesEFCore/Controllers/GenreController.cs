using Microsoft.AspNetCore.Mvc;

namespace manipulationDonneesEFCore.Controllers
{
    public class GenreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
