using Microsoft.AspNetCore.Mvc;
using manipulationDonneesEFCore.Data;
using manipulationDonneesEFCore.Models;

namespace manipulationDonneesEFCore.Controllers
{
    public class MembershipTypeController : Controller
    {
        private readonly AppDbContext _context;

        public MembershipTypeController(AppDbContext context)
        {
            _context = context;
        }

        // Liste des memberships
        public IActionResult Index()
        {
            var memberships = _context.MembershipTypes.ToList();
            return View(memberships);
        }

        // Créer un membership GET
        public IActionResult Create()
        {
            return View();
        }

        // Créer un membership POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MembershipType membership)
        {
            ModelState.Remove("Customers"); // ← ajoute cette ligne

            if (ModelState.IsValid)
            {
                _context.Add(membership);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return View(membership);
        }

        // Supprimer
        public IActionResult Delete(int id)
        {
            var membership = _context.MembershipTypes.Find(id);
            if (membership == null)
                return NotFound();

            _context.MembershipTypes.Remove(membership);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}