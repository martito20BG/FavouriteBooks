using FavoriteBooks.Data;
using FavoriteBooks.Data.Models;
using FavoriteBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteBooks.Controllers
{
    public class GenreController : Controller
    {
        private ApplicationDbContext db;

        public GenreController(ApplicationDbContext db) 
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
       [HttpPost]
        public IActionResult Add(GenreViewModel model)
        {
            var genre = new Genre
            {
                Name = model.Name
            };
            db.Genres.Add(genre);
            db.SaveChanges();
            return Redirect("/Home/Index");
        }
    }
}
