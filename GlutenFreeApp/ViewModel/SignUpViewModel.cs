using GlutenFreeApp.ViewModel;
using System.Windows.Input;
using GlutenFreeApp.Services;
using GlutenFreeApp.Models;

namespace GlutenFreeApp.ViewModel;

public class SignUpViewModel : ViewModelBase
{
    #region properties

    private string restName;
    public string RestName
    {
        get {return restName;}  
        set {restName = value; OnPropertyChanged(); }
    }

    
    private string username;
    public string Username
    {
        get { return username; }
        set { username = value; OnPropertyChanged(); }
    }

    private string address;
    public string Address
    {
        get { return address; }
        set { address = value; OnPropertyChanged(); }

    }

    private int typeFood;
    public int TypeFood
    {
        get { return typeFood; }
        set { typeFood = value; OnPropertyChanged(); }
    }
    #endregion

    //ולידציה לסיסמה
    #region Password
    

    private bool showPasswordError;

    public bool ShowPasswordError
    {
        get => showPasswordError;
        set
        {
            showPasswordError = value;
            OnPropertyChanged("ShowPasswordError");
        }
    }

    private string password;

    public string Password
    {
        get => password;
        set
        {
            password = value;
            ValidatePassword();
            OnPropertyChanged("Password");
        }
    }

    private string passwordError;

    public string PasswordError
    {
        get => passwordError;
        set
        {
            passwordError = value;
            OnPropertyChanged("PasswordError");
        }
    }

    private void ValidatePassword()
    {
        //Password must include characters and numbers and be longer than 4 characters
        if (string.IsNullOrEmpty(password) ||
            password.Length < 4 ||
            !password.Any(char.IsDigit) ||
            !password.Any(char.IsLetter))
        {
            this.ShowPasswordError = true;
        }
        else
            this.ShowPasswordError = false;
    }

    //This property will indicate if the password entry is a password
    private bool isPassword = true;
    public bool IsPassword
    {
        get => isPassword;
        set
        {
            isPassword = value;
            OnPropertyChanged("IsPassword");
        }
    }
    //This command will trigger on pressing the password eye icon
    public Command ShowPasswordCommand { get; }
    //This method will be called when the password eye icon is pressed
    public void OnShowPassword()
    {
        //Toggle the password visibility
        IsPassword = !IsPassword;
    }
    #endregion

    //נחבר את זה לכפתורים
    #region buttons
    public ICommand ManagerSelectedCommand { get; set; }
    public ICommand VisitorSelectedCommand { get; set; }
    public ICommand SubmitCommand { get; set; }
    #endregion

    #region email

    private string userEmail;
    public string UserEmail
    {
        get { return userEmail; }
        set 
        { 
            userEmail = value;
            ValidateEmail();
            OnPropertyChanged(); 
        }
    }

    private string emailError;

    public string EmailError
    {
        get => emailError;
        set
        {
            emailError = value;
            OnPropertyChanged("EmailError");
        }
    }

    private bool showEmailError;

    public bool ShowEmailError
    {
        get => showEmailError;
        set
        {
            showEmailError = value;
            OnPropertyChanged("ShowEmailError");
        }
    }


    private void ValidateEmail()
    {
        this.ShowEmailError = string.IsNullOrEmpty(UserEmail);
        if (!ShowEmailError)
        {
            //check if email is in the correct format using regular expression
            if (!System.Text.RegularExpressions.Regex.IsMatch(UserEmail, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                EmailError = "Email is not valid";
                ShowEmailError = true;
            }
            else
            {
                EmailError = "";
                ShowEmailError = false;
            }
        }
        else
        {
            EmailError = "Email is required";
        }
    }
    #endregion

    private GlutenFreeServiceWebAPIProxy proxy;
    public SignUpViewModel(GlutenFreeServiceWebAPIProxy proxy)
    {
        //הפעולות שמשנות את האנטריז בהתאם

        this.proxy = proxy;
        ManagerSelectedCommand = new Command(ManagerSelected, () => !IsManager);
        VisitorSelectedCommand = new Command(VisitorSelected, () => IsManager);
        SubmitCommand = new Command(OnRegister);
        IsManager = false;
        EmailError = "Email is required";
        PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
        UploadPhotoCommand = new Command(OnUploadPhoto);
        PhotoURL = proxy.GetDefaultRestaurantPhotoUrl();
        LocalPhotoPath = "";
    }

    #region regsiter
    private async void OnRegister()
    {
        ValidatePassword();
       
        if (!ShowPasswordError)
        {

            //NEXT ETERATION - REGISTER FOR MANAGER 

            //register for manager

            if (IsManager)
            {

                //Create a new user that is a manager
                var newUser = new UsersInfo
                {
                    Name = this.Username,
                    Password = this.Password,
                    TypeID = 3,
                    UserEmail = this.UserEmail,
                    UserID = 0
                };
                //Create the restaurant
                var newRest = new RestaurantInfo
                {
                    RestAddress = this.Address,
                    StatusID = 2,//PENDING
                    TypeFoodID = this.TypeFood + 1,
                    UserID = (int)newUser.UserID,
                    RestName = this.RestName
                };
                var manager = new ManagerInfo
                {
                    UserManager = newUser,
                    RestaurantManager = newRest
                };

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                manager = await proxy.RegisterManager(manager);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newUser != null)
                {
                    //UPload profile imae if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        //login so we can upload the picture
                        await proxy.LoginAsync(new UsersInfo { UserEmail = newUser.UserEmail, Password = newUser.Password, Name = newUser.Name, TypeID = 3 });
                        Restaurantid = manager.RestaurantManager.RestID;
                        //add an error
                        RestaurantInfo? updatedRestaurant = await proxy.UploadRestaurantImage(LocalPhotoPath, Restaurantid);
                        if (updatedRestaurant == null)
                        {
                            InServerCall = false;
                            await Application.Current.MainPage.DisplayAlert("Registration", "User Data Was Saved BUT Restaurant image upload failed", "ok");
                        }
                        InServerCall = false;
                        await Application.Current.MainPage.DisplayAlert("Registration Success", "you are now going to the login page", "ok");
                        ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                    }


                }
            }

            //if its not a manager
            if (!IsManager)
            {
                var newUser = new UsersInfo
                {
                    Name = this.Username,
                    Password = this.Password,
                    TypeID = 1,
                    UserEmail = this.UserEmail
                };

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                newUser = await proxy.RegisterRegular(newUser);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newUser != null)
                {
                    InServerCall = false;


                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
            }

        }
        else
        {

            //If the registration failed, display an error message
            string errorMsg = "Registration failed. Please try again.";
            await Application.Current.MainPage.DisplayAlert("Registration", "failed", "ok");
        }

    }
    #endregion

    #region Photo Restaurant
    //this holds the restaurant id after the user uploaded it so we can sort it in the files
    private int restaurantid;
    public int Restaurantid
    {
        get => restaurantid;
        set
        {
            if (restaurantid != value)
            {
                restaurantid = value;
                OnPropertyChanged();
            }
        }
    }

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
    //This method open the file picker to select a photo and saves the photo but doesnt upload it yet
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


        }
        catch (Exception ex)
        {
        }

    }
    #endregion

    #region selection of type 
    private bool isManager;
    public bool IsManager
    {
        get { return isManager; }
        set 
        {
            isManager = value;      
            OnPropertyChanged(); 
            ((Command)ManagerSelectedCommand).ChangeCanExecute();
            ((Command)VisitorSelectedCommand).ChangeCanExecute();
        }
    }

    
    private async void ManagerSelected()
    {
        IsManager=true; 
    }

    private async void VisitorSelected()
    {
        IsManager=false;
    }
    #endregion

}