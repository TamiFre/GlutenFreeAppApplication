using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutenFreeApp.Models
{
    public class RestaurantInfo
    {
        public string Recipe { get; set; } = null;
        public int RecipeID { get; set; }
        public int UserID { get; set; }
    }
}
