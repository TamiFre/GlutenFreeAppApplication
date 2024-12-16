using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutenFreeApp.Models
{
    public class ManagerInfo
    {
        public UsersInfo UserManager { get; set; } = null;
        public RestaurantInfo RestaurantManager { get; set; } = null;
    }
}
