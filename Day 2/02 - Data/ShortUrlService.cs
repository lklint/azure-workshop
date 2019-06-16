using System.Threading.Tasks;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private Persistence _persistence;

        public ShortUrlService(Persistence persistence)
        {
            _persistence = persistence;
        }

        public async Task<ShortUrl> GetByIdAsync(int id)
        {
            return await _persistence.GetByIdAsync(id);
        }

        public async Task<ShortUrl> GetByPathAsync(string path)
        {
            return await _persistence.GetUrlByPathAsync(path);
        }

        public async Task<ShortUrl> GetByOriginalUrlAsync(string originalUrl)
        {
            return await _persistence.GetUrlByPathAsync(originalUrl);
        }

        public async Task<int> SaveAsync(ShortUrl shortUrl)
        {
            await _persistence.SaveUrlAsync(shortUrl);

            return shortUrl.UrlId;
        }
    }
}
