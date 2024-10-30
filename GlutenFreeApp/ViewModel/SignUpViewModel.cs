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
    private bool isManagerSelected;

   

    public bool IsManagerSelected
    {
        get { return isManagerSelected; }
        set
        {

            isManagerSelected = value;
            ((Command)ManagerSelectedCommand).ChangeCanExecute();
            ((Command)VisitorSelectedCommand).ChangeCanExecute();
            OnPropertyChanged();

        }
    }



    public ICommand ManagerSelectedCommand { get; }
    public ICommand VisitorSelectedCommand { get; }

    public SignUpViewModel()
    {

        ManagerSelectedCommand = new Command(() => IsManagerSelected = true,()=>!isManagerSelected);
        VisitorSelectedCommand = new Command(() => IsManagerSelected = false,()=>isManagerSelected);
        IsManagerSelected = false;

    }

    private void OnManagerSelected()
    {
        IsManagerSelected = true;
        

    }

    private void OnVisitorSelected()
    {
        IsManagerSelected = false;

        
    }

  
}