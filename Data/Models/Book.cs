namespace FavoriteBooks.Data.Models
{
    public class Book
    {
        public Book()
        {
            this.Images = new HashSet<Image>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PagesCount { get; set; }
        public int GenreId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
