using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

namespace BLL.Models
{
    public class DirectorModel
    {
        public Director Record { get; set; }

        public string Name => Record.Name;

        public string Surname => Record.Surname;

        public string FullName => Record.Name + " " + Record.Surname;

        public string IsRetired => Record.IsRetired ? "Yes" : "No";


    }
}
