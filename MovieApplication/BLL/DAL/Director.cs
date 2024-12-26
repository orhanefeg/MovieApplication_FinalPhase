using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Director
    {
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string Name { get; set; }
        public string Surname { get; set; }

        public bool IsRetired {  get; set; }

        public List<Movie> Movies{ get; set; } = new List<Movie>();


    }
}