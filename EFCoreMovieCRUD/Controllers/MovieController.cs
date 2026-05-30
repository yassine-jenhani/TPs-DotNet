using EFCoreMovieCRUD.Data;
using EFCoreMovieCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovieCRUD.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDBContext _db;

        public MovieController(ApplicationDBContext db)
        {
            _db = db;
        }

        // GET: /Movie/Index
        public IActionResult Index(string sortOrder, int page = 1)
        {
            int pageSize = 5;

            // Paramètres de tri transmis à la vue
            ViewBag.NameSort = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewBag.SortOrder = sortOrder;

            var movies = _db.Movies.Include(m => m.Genre).AsQueryable();

            // Tri dynamique
            movies = sortOrder switch
            {
                "name_asc" => movies.OrderBy(m => m.Name),
                "name_desc" => movies.OrderByDescending(m => m.Name),
                _ => movies.OrderBy(m => m.Id)
            };

            // Pagination
            int totalItems = movies.Count();
            var items = movies
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(items);
        }

        // GET: /Movie/Details/5
        public IActionResult Details(int id)
        {
            var movie = _db.Movies
                           .Include(m => m.Genre)
                           .FirstOrDefault(m => m.Id == id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        // GET: /Movie/Create
        public IActionResult Create()
        {
            ViewBag.Genres = new SelectList(_db.Genres.ToList(), "Id", "Name");
            return View();
        }

        // POST: /Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _db.Movies.Add(movie);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Genres = new SelectList(_db.Genres.ToList(), "Id", "Name", movie.GenreId);
            return View(movie);
        }

        // GET: /Movie/Edit/5
        public IActionResult Edit(int id)
        {
            var movie = _db.Movies.Find(id);
            if (movie == null) return NotFound();

            ViewBag.Genres = new SelectList(_db.Genres.ToList(), "Id", "Name", movie.GenreId);
            return View(movie);
        }

        // POST: /Movie/Edit/5
        // POST: /Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                // Récupérer le film existant depuis la base
                var existingMovie = _db.Movies.Find(movie.Id);
                if (existingMovie == null) return NotFound();

                // Mettre à jour uniquement les champs nécessaires
                existingMovie.Name = movie.Name;
                existingMovie.GenreId = movie.GenreId;

                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Genres = new SelectList(_db.Genres.ToList(), "Id", "Name", movie.GenreId);
            return View(movie);
        }

        // GET: /Movie/Delete/5
        public IActionResult Delete(int id)
        {
            var movie = _db.Movies
                           .Include(m => m.Genre)
                           .FirstOrDefault(m => m.Id == id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        // POST: /Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _db.Movies.Find(id);
            if (movie != null)
            {
                _db.Movies.Remove(movie);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}