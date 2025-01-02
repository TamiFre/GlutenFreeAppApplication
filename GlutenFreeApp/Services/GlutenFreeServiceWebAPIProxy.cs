using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GlutenFreeApp.Models;


namespace GlutenFreeApp.Services
{
    public class GlutenFreeServiceWebAPIProxy
    {
        #region without tunnel
        /*
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "localhost";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110/api/" : $"http://{serverIP}:5110/api/";
        private static string ImageBaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110" : $"http://{serverIP}:5110";
        */
        #endregion

        #region with tunnel
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "srxdcrp2-5199.euw.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://srxdcrp2-5199.euw.devtunnels.ms/api/";
        private static string ImageBaseAddress = "https://srxdcrp2-5199.euw.devtunnels.ms/";
        #endregion
        public GlutenFreeServiceWebAPIProxy()
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }


        #region Regular register

        //This methos call the Register web API on the server and return the AppUser object with the given ID
        //or null if the call fails
        public async Task<UsersInfo?> RegisterRegular(UsersInfo user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}RegisterRegular";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                  UsersInfo? result = JsonSerializer.Deserialize<UsersInfo>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region Login
        //Login

        public async Task<UsersInfo?> LoginAsync(UsersInfo userInfo)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}Login";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(userInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    UsersInfo? result = JsonSerializer.Deserialize<UsersInfo>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        //NEXT ETERATION

        #region Register manager
        public async Task<ManagerInfo?> RegisterManager(ManagerInfo manager)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}RegisterManager";

            try
            {
              
                //Call the server API
                string json = JsonSerializer.Serialize(manager);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    ManagerInfo? result = JsonSerializer.Deserialize<ManagerInfo>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region Add Fun Fact
        //will return true if added correctly and false otherwise
        public async Task<bool> AddFactAsync(InformationInfo fact)
        {
            string url = $"{this.baseUrl}AddFact";
            try 
            {
                //do a json to info
                string json = JsonSerializer.Serialize<InformationInfo>(fact);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //check if fine
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Add Recipe

        //Check
        public async Task<bool> AddRecipeAsync(RecipeInfo recipeInfo)
        {
            string url = $"{this.baseUrl}AddRecipe";
            try
            {
                //do a json to info
                string json = JsonSerializer.Serialize<RecipeInfo>(recipeInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //check if fine
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Get Restaurants By Status
       
        public async Task<List<RestaurantInfo>> GetRestaurantByStatus(int statusID)
        {
            string url = $"{this.baseUrl}GetRestByStatus?statusID={statusID}";  // Correct query string for GET request

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);  // Use GET instead of POST
                if (response.IsSuccessStatusCode)
                {
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<RestaurantInfo> result = JsonSerializer.Deserialize<List<RestaurantInfo>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region Get All Restaurants
        //get all restaurants
        public async Task<List<RestaurantInfo>> GetAllRestaurants()
        {
            string url = $"{this.baseUrl}GetAllRests";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();

                    // Deserialize result to List<GaragePartsDTO>
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<RestaurantInfo> result = JsonSerializer.Deserialize<List<RestaurantInfo>>(resContent, options);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Get Recipes By Status
        public async Task<List<RecipeInfo>> GetRecipetByStatus(int statusID)
        {
            string url = $"{this.baseUrl}GetRecipeByStatus?statusID={statusID}";  // Correct query string for GET request

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);  // Use GET instead of POST
                if (response.IsSuccessStatusCode)
                {
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<RecipeInfo> result = JsonSerializer.Deserialize<List<RecipeInfo>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Get All Recipes
        public async Task<List<RecipeInfo>> GetAllRecipes()
        {
            string url = $"{this.baseUrl}GetAllRecipes";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();

                    // Deserialize result to List<GaragePartsDTO>
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<RecipeInfo> result = JsonSerializer.Deserialize<List<RecipeInfo>>(resContent, options);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Get All Approved Recipes
        public async Task<List<RecipeInfo>> GetAllApprovedRecipes()
        {
            string url = $"{this.baseUrl}GetAllApprovedRecipes";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();

                    // Deserialize result to List<GaragePartsDTO>
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<RecipeInfo> result = JsonSerializer.Deserialize<List<RecipeInfo>>(resContent, options);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Get all Approved Restaurants
        public async Task<List<RestaurantInfo>> GetAllApprovedRestaurants()
        {
            string url = $"{this.baseUrl}GetAllApprovedRestaurants";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    // Deserialize result to List<GaragePartsDTO>
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<RestaurantInfo> result = JsonSerializer.Deserialize<List<RestaurantInfo>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Get Approved Restaurant By Food Type Selected
        public async Task<List<RestaurantInfo>> GetApprovedRestaurantsByChosenFoodType(int chosenFoodType)
        {
            string url = $"{this.baseUrl}GetApprovedRestaurantsByChosenFoodType?chosenFoodType={chosenFoodType}";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    // Deserialize result to List<GaragePartsDTO>
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<RestaurantInfo> result = JsonSerializer.Deserialize<List<RestaurantInfo>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Get All Critics For A Specific Restaurant
        public async Task<List<CriticInfo>?> GetCriticForRestaurant(RestaurantInfo restaurantInfo)
        {
            string url = $"{this.baseUrl}GetCriticForRestaurant";
            try
            {
                //do a json to info
                string json = JsonSerializer.Serialize<RestaurantInfo>(restaurantInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //check if fine
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<CriticInfo>? result = JsonSerializer.Deserialize<List<CriticInfo>?>(resContent, options);
                    return result;
                }

                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Get Approved Recipes By Food Type Selection
        public async Task<List<RecipeInfo>?> GetApprovedRecipesByChosenFoodType(int chosenFoodType)
        {
            string url = $"{this.baseUrl}GetApprovedRecipesByChosenFoodType?chosenFoodType={chosenFoodType}";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    // Deserialize result to List<GaragePartsDTO>
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<RecipeInfo> result = JsonSerializer.Deserialize<List<RecipeInfo>>(resContent, options);
                    return result;
                }
                return null;
            }
            catch (Exception ex) 
            {
                return null;
            }
        }
        #endregion
    }
}
