using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IShortUrlService
    {
        Task<ShortUrl> GetByIdAsync(int id);

        Task<ShortUrl> GetByPathAsync(string path);

        Task<ShortUrl> GetByOriginalUrlAsync(string originalUrl);

        Task<int> SaveAsync(ShortUrl shortUrl);
    }
}
