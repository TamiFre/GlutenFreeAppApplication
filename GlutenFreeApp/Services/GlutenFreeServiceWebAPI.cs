using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GlutenFreeApp.Models;

namespace GlutenFreeApp.Services
{
    public class GlutenFreeServiceWebAPI
    {
        //מנהל תכונות מתקדמות של בקשות HTTP
        //cookies כמו תמיכה
        private HttpClient client;

        // JSON משתנה זה יכיל את ההגדרות שייקבעו בהמשך כיצד לעבד ולהמיר נתוני
        // בעת שליחת וקבלת בקשות מהשרת
        private JsonSerializerOptions jsonSerializerOptions;


        //לא לשכוח לשנות לפי ההמשך כשהסרבר יעבוד

        // כתובת הבסיס לכתובת השרת מותאמת לפי פלטפורמות ההרצה
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://59zm7fqh-5021.uks1.devtunnels.ms/api/" : "http://localhost:5021/api/";
        public static string ImageUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://59zm7fqh-5021.uks1.devtunnels.ms/images/" : "http://localhost:5021/images/";




        // אובייקט של מחלקת השירות שמכיל את כתובת הבסיס לשרת
        private string baseUrl;


        //מאפיין זה מחזיק את פרטי המשתמש לאחר התחברות מוצלחת.
        //ניתן להשתמש בו לצורך בדיקה או שליפה של מידע על המשתמש המחובר
        public UsersInfo LoggedInUser { get; set; }


        public GlutenFreeServiceWebAPI()
        {
            // Set up the HTTP client handler to support cookies
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer(); // Initialize a container to store cookies

            // Create a new HttpClient with the handler and enable automatic disposal of the handler
            this.client = new HttpClient(handler, true);

            // Set the base URL for API requests
            this.baseUrl = BaseAddress;

            // Configure JSON serializer options to make JSON formatting more readable and case insensitive
            jsonSerializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true, // Makes the JSON output more readable (with indents)
                PropertyNameCaseInsensitive = true // Allows deserialization to ignore property name case differences
            };
        }


    }
}
