﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutenFreeApp.Models
{
    public class RestaurantInfo
    {
        public string RestAddress { get; set; } = null;
        public string RestName { get; set; } = null;
        public int RestID { get; set; }
        public int UserID { get; set; }
        public int StatusID { get; set; }  
        public int TypeFoodID { get; set; }
    }
}
