using manipulationDonneesEFCore.Data;
using manipulationDonneesEFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace manipulationDonneesEFCore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var customers = _context.Customers
                .Include(c => c.MembershipType)
                .ToList();
            return View(customers);
        }

        public IActionResult Create()
        {
            // Charger la liste des memberships pour le dropdown
            ViewBag.MembershipTypes = new SelectList(
                _context.MembershipTypes, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            // Ignorer les erreurs de navigation properties
            ModelState.Remove("MembershipType");
            ModelState.Remove("Movies");

            if (ModelState.IsValid)
            {
                _context.Add(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            ViewBag.MembershipTypes = new SelectList(
                _context.MembershipTypes, "Id", "Id");

            return View(customer);
        }
    }
}
