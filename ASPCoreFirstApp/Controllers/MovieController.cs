using Microsoft.AspNetCore.Mvc;
using ASPCoreFirstApp.Models;
using ASPCoreFirstApp.ViewModels;

namespace ASPCoreFirstApp.Controllers
{
    public class MovieController : Controller
    {
        // Action principale (Question 2 du TP)
        public IActionResult Index()
        {
            var movie = new Movie { Id = 1, Name = "Inception" };
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Yassine" },
                new Customer { Id = 2, Name = "Shadi" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        // Action Détails (Question 2 - "Récupérer détails par Id")
        public IActionResult Details(int id)
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Yassine" },
                new Customer { Id = 2, Name = "Shadi" }
            };

            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // Action avec Attribute Routing (Question 1)
        [Route("Movie/released/{year}/{month:range(1, 12)}")]
        public IActionResult ByRelease(int year, int month)
        {
            return Content($"Filtre appliqué - Année: {year}, Mois: {month}");
        }
    }
}