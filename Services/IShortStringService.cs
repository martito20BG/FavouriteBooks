namespace FavoriteBooks.Services
{
    public interface IShortStringService
    {
      public  string GetShort(string str, int maxLen);
    }
}
