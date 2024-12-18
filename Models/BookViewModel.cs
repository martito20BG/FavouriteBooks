using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FavoriteBooks.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        public string Author { get; set; }
        public List<SelectListItem> Genres { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public IFormFile Image { get; set; }
        public string ImageURL { get; set; }
    }
}
