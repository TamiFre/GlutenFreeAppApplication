using System;
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
        public string ProfileImagePath { get; set; } = "";
        public string FullImagePath
        {
            get
            {
                return GlutenFreeServiceWebAPIProxy.ImageBaseAddress + this.ProfileImagePath;
            }
        }
        public string StatusName
        {
            get
            {
                List<StatusInfo> statuses = ((App)Application.Current).Statuses;
                StatusInfo status = statuses.Where(s => s.StatusID == this.StatusID).FirstOrDefault();
                if (status != null)
                    return status.StatusDesc;
                return "Unknown!";
            }
        }
        public int TypeFoodID { get; set; }
        public string TypeFoodName
        {
            get
            {
                List<TypeFoodInfo> foodTypes = ((App)Application.Current).FoodTypes;
                TypeFoodInfo foodType = foodTypes.Where(f => f.TypeFoodID == this.TypeFoodID).FirstOrDefault();
                if (foodType != null)
                    return foodType.TypeFoodName;
                return "Unknown";
            }
        }

        //CHECK IN CLASS
        public bool IsSwipeEnabled
        {
            get
            {
                if (this.StatusID == 2)
                    return true;
                return false;
            }
        }
    }
}
