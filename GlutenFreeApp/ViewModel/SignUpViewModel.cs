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

    public string password;
    public string Password
    {
        get { return password; }
        set { password = value; OnPropertyChanged(); }
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



    //private bool isManagerSelected;

   

    //public bool IsManagerSelected
    //{
    //    get { return isManagerSelected; }
    //    set
    //    {

    //        isManagerSelected = value;
    //        ((Command)ManagerSelectedCommand).ChangeCanExecute();
    //        ((Command)VisitorSelectedCommand).ChangeCanExecute();
    //        OnPropertyChanged();

    //    }
    //}



    //public ICommand ManagerSelectedCommand { get; }
    //public ICommand VisitorSelectedCommand { get; }

    //public SignUpViewModel()
    //{

    //    ManagerSelectedCommand = new Command(() => IsManagerSelected = true,()=>!isManagerSelected);
    //    VisitorSelectedCommand = new Command(() => IsManagerSelected = false,()=>isManagerSelected);
    //    IsManagerSelected = false;

    //}

    //private void OnManagerSelected()
    //{
    //    IsManagerSelected = true;
        

    //}

    //private void OnVisitorSelected()
    //{
    //    IsManagerSelected = false;

        
    //}

  
}