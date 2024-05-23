using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Expert
    {
        public int Id { get; set; }
        [MinLength(5)]
        [MaxLength(25)]
        public string Name { get; set; }
        [MinLength(5)]
        [MaxLength(25)]
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile formFile { get; set; }
    }
}
