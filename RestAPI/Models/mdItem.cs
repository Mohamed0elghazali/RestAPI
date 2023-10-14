using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models
{
    public class mdItem
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public double Price { get; set; }

        public string? Notes { get; set; }

        public IFormFile Image { get; set; }

        public int CategoryID { get; set; }

    }
}
