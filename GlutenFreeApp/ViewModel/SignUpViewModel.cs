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

    private string typeFood;
    public string TypeFood
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

    private GlutenFreeServiceWebAPIProxy proxy;
    public SignUpViewModel(GlutenFreeServiceWebAPIProxy proxy)
    {
        //הפעולות שמשנות את האנטריז בהתאם

        this.proxy = proxy;
        ManagerSelectedCommand = new Command(ManagerSelected, () => !IsManager);
        VisitorSelectedCommand = new Command(VisitorSelected, () => IsManager);
        SubmitCommand = new Command(OnRegister);
        IsManager = false;
        PasswordError = "Password must be at least 4 characters long and contain letters and numbers";

    }

    
    //HOW TO CHANGE IF IS MANAGER

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
                        UserID = 0
                    };
                    //Create the restaurant
                    var newRest = new RestaurantInfo
                    {
                        Address = this.Address,
                        StatusID = 2,//PENDING
                        TypeFoodID = 2,//not working
                        UserID =(int)newUser.UserID
                    };
                var manager = new ManagerInfo
                {
                    UserInfo = newUser,
                    RestInfo = newRest
                };

                    //Call the Register method on the proxy to register the new user
                    InServerCall = true;
                    manager = await proxy.RegisterManager(manager);
                    InServerCall = false;

                    //If the registration was successful, navigate to the login page
                    if (newUser != null)
                    {
                        InServerCall = false;

                        //ASK OFER

                        ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                    }

                }




            //if its not a manager


            if (!IsManager)
            {
                var newUser = new UsersInfo
                {
                    Name = this.Username,
                    Password = this.Password,
                    TypeID = 1
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