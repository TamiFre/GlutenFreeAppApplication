using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlutenFreeApp.Models
{
    public class CriticInfo
    {
        public int CriticID { get; set; }
        public string CriticText { get; set; } = null;
        public int? UserID { get; set; }
        public int? RestID { get; set; }
    }
}
