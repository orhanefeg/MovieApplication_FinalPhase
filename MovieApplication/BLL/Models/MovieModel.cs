using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

namespace BLL.Models
{
    public class MovieModel
    {
        public Movie Record { get; set; }

        public string Name => Record.Name;

        [DisplayName("Release Date")]
        public string ReleaseDate => !Record.ReleaseDate.HasValue ? string.Empty : Record.ReleaseDate.Value.ToString("MM/dd/yyyy");

        public string TotalRevenue => Record.TotalRevenue.HasValue ? Record.TotalRevenue.Value.ToString("C") : "0";

        //one to many
        public string Directors => Record.Director?.Name;

        //many to many

        public string Genres => string.Join("<br>", Record.MovieGenres?.Select(mg => mg.Genre?.Name));

        [DisplayName("Genres")]
        public List<int> GenreIds 
        {
            
            get => Record.MovieGenres?.Select(mg => mg.GenreId).ToList();
            set => Record.MovieGenres = value.Select(v => new MovieGenre() {GenreId = v }).ToList();

        }
    }
}
