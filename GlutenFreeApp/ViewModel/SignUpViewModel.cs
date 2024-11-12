using GlutenFreeApp.ViewModel;
using System.Windows.Input;
namespace GlutenFreeApp.ViewModel;

public class SignUpViewModel : ViewModelBase
{
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

    public SignUpViewModel()
    {
     
        ManagerSelectedCommand = new Command(ManagerSelected,()=>!IsManager);
        VisitorSelectedCommand = new Command(VisitorSelected,()=>IsManager);
        IsManager = false;

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