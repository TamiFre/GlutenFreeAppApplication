using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GlutenFreeApp.Models;
using GlutenFreeApp.Services;

namespace GlutenFreeApp.ViewModel
{
    public class AddRecipeViewModel:ViewModelBase
    {
        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        #region builder
        public AddRecipeViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            SubmitRecipeCommand = new Command(AddRecipe);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();
            LocalPhotoPath = "";

        }
        #endregion

        #region Photo

        private string photoURL;

        public string PhotoURL
        {
            get => photoURL;
            set
            {
                photoURL = value;
                OnPropertyChanged("PhotoURL");
            }
        }

        private string localPhotoPath;

        public string LocalPhotoPath
        {
            get => localPhotoPath;
            set
            {
                localPhotoPath = value;
                OnPropertyChanged("LocalPhotoPath");
            }
        }

        public Command UploadPhotoCommand { get; }
        //This method open the file picker to select a photo

        private int recipeid;
        public int Recipeid
        {
            get => recipeid;
            set 
            {
                if (recipeid != value)
                {
                    recipeid = value;
                    OnPropertyChanged();
                }
            }
        }
        private async void OnUploadPhoto()
        {
            try
            {
                
               
                var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo",
                });

                if (result != null)
                {
                    // The user picked a file
                    this.LocalPhotoPath = result.FullPath;
                    this.PhotoURL = result.FullPath;
                }

                RecipeInfo? updatedrecipe = await proxy.UploadRecipeImage(LocalPhotoPath, Recipeid);
            }
            catch (Exception ex)
            {
            }

        }
        #endregion

        #region Properties
        private string recipe;
        public string Recipe
        {
            get { return recipe; }
            set { recipe = value; OnPropertyChanged(); }
        }

        private string errorMsg;
        public string ErrorMsg
        {
            get => errorMsg;
            set
            {
                if (errorMsg != value)
                {
                    errorMsg = value;
                    OnPropertyChanged(nameof(ErrorMsg));
                }
            }
        }

        private int typeFood;
        public int TypeFood
        {
            get { return typeFood; }
            set { typeFood = value; OnPropertyChanged(); }
        }
        #endregion

        #region buttons
        public ICommand SubmitRecipeCommand { get; set; }
        #endregion

       
        private async void AddRecipe()
        {
            InServerCall = true;

            UsersInfo? u = ((App)Application.Current).LoggedInUser;
            //call the server to add the information

            //add the recipe as pending

            //how to add user id 
            RecipeInfo information = new RecipeInfo { RecipeText = Recipe, StatusID=2, UserID = u.UserID.Value, TypeFoodID = TypeFood};
            RecipeInfo worked = await this.proxy.AddRecipeAsync(information);
            InServerCall = false;

            if (worked==null)
            {
                ErrorMsg = "Something Went Wrong";
            }
            else
            {
                Recipeid=worked.RecipeID;
                ErrorMsg = "All Good";
            }

        }
       
    }
}
