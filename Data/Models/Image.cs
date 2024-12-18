namespace FavoriteBooks.Data.Models
{
    public class Image
    {
        public Image() 
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
