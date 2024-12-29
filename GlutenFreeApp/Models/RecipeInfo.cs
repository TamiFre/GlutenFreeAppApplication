using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutenFreeApp.Models
{
    public class RecipeInfo
    {
        public string RecipeText { get; set; } = null;
        public int RecipeID { get; set; }
        public int UserID { get; set; }
        public int StatusID { get; set; }
    }
}
