using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL.DAL
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }   

        public decimal? TotalRevenue {  get; set; }

        [Required(ErrorMessage = "Director part cant be blank!!!!")]
        public int DirectorId { get; set; }
   
        public Director Director { get; set; }

        public List<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>(); 
    }
}
