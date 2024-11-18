using GlutenFreeApp.ViewModel;
using System.Windows.Input;
using GlutenFreeApp.Services;
using GlutenFreeApp.Models;
namespace GlutenFreeApp.ViewModel;

public class SignUpViewModel : ViewModelBase
{
    //התכונות
    private string username;
    public string Username
    {
        get { return username; }
        set { username = value; OnPropertyChanged(); }
    }

    private string adress;
    public string Adress
    {
        get { return adress; }
        set { adress = value; OnPropertyChanged(); }

    }


    private string? passError;
    public string PassError
    {
        get { return passError; }
        set 
        {
            passError = value; OnPropertyChanged();
        }
    }


    //ולידציה לסיסמה

    private string password;
    public string? Password
    {
        get { return password; }

        set 
        {
            password = value;
            PassError = "";
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(PassError));
            if (string.IsNullOrEmpty(password))
            {
                PassError = "";
            }
            else
            {
                if (password != null)
                {
                    bool IsPasswordOkay = IsValidPassword(password);
                    if (!IsPasswordOkay)
                    {
                        PassError = "סיסמה לא טובה ";
                    }
                }
                
            }
        }
    }

   
    private bool IsValidPassword(string password)
    {
        bool hasUpperCase = false;
        bool hasDigit = false;
        
        foreach (char c in password)
        {
            if (char.IsUpper(c))
            {
                hasUpperCase = true;
            }
            if (char.IsDigit(c))
            {
                hasDigit = true;
            }

            if (hasUpperCase && hasDigit)
            {
                break; // אם מצאנו כבר גם אות גדולה וגם ספרה, אפשר לעצור את הלולאה
            }
        }
        return hasUpperCase && hasDigit;

    }

    //נחבר את זה לכפתורים
    public ICommand ManagerSelectedCommand { get; set; }
    public ICommand VisitorSelectedCommand { get; set; }
    public ICommand SubmitCommand { get; set; }

    private GlutenFreeServiceWebAPIProxy proxy;
    public SignUpViewModel(GlutenFreeServiceWebAPIProxy proxy)
    {
        //הפעולות שמשנות את האנטריז בהתאם

        this.proxy = proxy;
        ManagerSelectedCommand = new Command(ManagerSelected, () => !IsManager);
        VisitorSelectedCommand = new Command(VisitorSelected, () => IsManager);
        SubmitCommand = new Command(OnRegister);
        IsManager = false;

    }


    //ADD MODELSBL AND THE NEEDED THINGS BELOW




    private async void OnRegister()
    {

        var newUser = new UsersInfo
        {
            Name = this.Username,
            Password = this.Password,
            //need to add the actions in ModelsBl
            TypeID = this.GetType().Name,
            UserID = this.GetID.Name
        };

        //the server is in action
        InServerCall = true;
        //call the proxy and the register with new user
        newUser = await proxy.Register(newUser);
        //we're done with the server call so enable
        InServerCall=false;

        //check if registeration okay
        if (newUser != null)
        {
            InServerCall = false;
            await Application.Current.MainPage.DisplayAlert("Registration", "User Data Was Saved", "ok");

            InServerCall = false;
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }
        else 
        {
            //display not good alert
            string errorMsg = "Registration failed. Please try again.";
            await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
        }

    }


    //נחבר את זה אם המנהל הוא האופציה הנבחרת
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


}