using EFCoreMovieCRUD.Data;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreMovieCRUD.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDBContext _db;
        public CustomerController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
