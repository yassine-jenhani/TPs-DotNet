using EFCoreMovieCRUD.Data;
using EFCoreMovieCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreMovieCRUD.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationDBContext _db;

        public GenreController(ApplicationDBContext db)
        {
            _db = db;
        }

        // GET: /Genre/Index
        public IActionResult Index()
        {
            List<Genre> objGenreList = _db.Genres.ToList();
            return View(objGenreList);
        }

        // GET: /Genre/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Genre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _db.Genres.Add(genre);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: /Genre/Edit/id
        public IActionResult Edit(Guid id)
        {
            var genre = _db.Genres.Find(id);
            if (genre == null) return NotFound();
            return View(genre);
        }

        // POST: /Genre/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _db.Genres.Update(genre);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: /Genre/Delete/id
        public IActionResult Delete(Guid id)
        {
            var genre = _db.Genres.Find(id);
            if (genre == null) return NotFound();
            return View(genre);
        }

        // POST: /Genre/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var genre = _db.Genres.Find(id);
            if (genre != null)
            {
                _db.Genres.Remove(genre);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
