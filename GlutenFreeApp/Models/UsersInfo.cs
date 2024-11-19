using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutenFreeApp.Models
{
    public class UsersInfo
    {
        public string Name { set; get; } = null;
        public string Password { set; get; } = null;
        public int? TypeID { set; get; }
        public int? UserID { set; get; }



    }
}
