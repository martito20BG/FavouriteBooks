using FavoriteBooks.Data;
using FavoriteBooks.Data.Models;
using FavoriteBooks.Models;
using FavoriteBooks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FavoriteBooks.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IShortStringService service;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BookController(ApplicationDbContext db, IShortStringService service, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.service = service;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            List<SelectListItem> genres = db.Genres.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            BookViewModel model = new BookViewModel();
            model.Genres = genres;
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(BookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            Book book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                GenreId = model.GenreId
            };

            string extension = Path.GetExtension(model.Image.FileName).TrimStart('.');
            Image dbImage = new Image
            {
                Extension = extension,
                Name = " "
            };
            string filePath = $"{webHostEnvironment.WebRootPath}/Images/{dbImage.Id}.{dbImage.Extension}";
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                model.Image.CopyTo(fs);
            }
            book.Images.Add(dbImage);
            db.Books.Add(book);
            db.SaveChanges();
            return Redirect("/Home/Index");
        }
        public IActionResult Details(int Id)
        {
            var bookModel = db.Books.Where(x => Id == x.Id).Select(x => new BookViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                ImageURL = $"/images/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension,


                Description = service.GetShort(x.Description, 40),
                GenreName = x.Genre.Name,
            }).FirstOrDefault();
            return View(bookModel);
        }
        public IActionResult All()
        {
            List<BookViewModel> model = db.Books.Select(x => new BookViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Description = x.Description,
                GenreId = x.GenreId,
                GenreName = x.Genre.Name
            }).ToList();
            return View(model);
        }

        public IActionResult Edit(int id) //update
        {
            Book book = db.Books.FirstOrDefault(x => x.Id == id);
            List<SelectListItem> genres = db.Genres.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            BookViewModel model = db.Books.Where(x => x.Id == id).Select(x => new BookViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                GenreId = x.GenreId,
                Description = x.Description,
                GenreName = x.Genre.Name,
                Genres = genres
            }).FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return View(model);
        }

            [HttpPost]
            public IActionResult Edit(BookViewModel model)
            {
                Book book = db.Books.FirstOrDefault(x => x.Id == model.Id);
                book.Title = model.Title;
                book.Author = model.Author; 
                book.GenreId = model.GenreId;
                book.Description = model.Description;
                db.SaveChanges();
                return Redirect("/Book/All");
            }

            public IActionResult Delete(int id)
            {
                Book book = db.Books.FirstOrDefault(x => x.Id == id);
                db.Books.Remove(book);
                db.SaveChanges();
                return Redirect("/Book/All");
            }
        }
    }