using manipulationDonneesEFCore.Data;
using manipulationDonneesEFCore.Models;
using manipulationDonneesEFCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace manipulationDonneesEFCore.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovieController(AppDbContext context,
                               IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieVM model, IFormFile photo)
        {
            ModelState.Remove("photo"); // ← optionnel si photo non requise

            if (photo == null)
            {
                ViewBag.Errors = new List<string> { "Veuillez choisir une image." };
                return View(model);
            }

            try
            {
                // Créer le dossier images s'il n'existe pas
                var imagesFolder = Path.Combine(
                    _webHostEnvironment.WebRootPath, "images");

                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                // Sauvegarder l'image
                var path = Path.Combine(imagesFolder, photo.FileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    photo.CopyTo(stream);
                    stream.Close();
                }

                // Mapper ViewModel vers entité Movie
                var movie = new Movie
                {
                    Id = Guid.NewGuid(),
                    Name = model.movie.Name,
                    DateAjoutMovie = model.movie.DateAjoutMovie,
                    ImageFile = photo.FileName,
                };

                _context.Add(movie);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Errors = new List<string> { ex.Message };
                return View(model);
            }
        }
    }
}
