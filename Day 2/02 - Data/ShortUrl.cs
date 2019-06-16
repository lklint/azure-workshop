using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class ShortUrl
    {
        public int UrlId { get; set; }
        
        [Required]
        public string OriginalUrl { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
