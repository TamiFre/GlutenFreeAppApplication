using System.Windows.Input;

namespace GlutenFreeApp.ViewModel;

public class LoginPageViewModel : ViewModelBase
{
    private string username;
    public string UserName
    {
        get => username;
        set
        {
            if (username != value)
            {
                username = value;
                OnPropertyChanged(nameof(Email));
                // can add more logic here like email validation etc.
                // can add error message property and set it here
            }
        }
    }
    private string password;
    public string Password
    {
        get => password;
        set
        {
            if (password != value)
            {
               
                password = value;
                OnPropertyChanged(nameof(Password));
                // can add more logic here like email validation etc.
                // can add error message property and set it here
            }
        }
    }

    //����� ����� �������� �������
    public ICommand TransferToSignUp { get; set; }

    private async void GoToSignUp()
    {
        AppShell.Current.GoToAsync("///SignUp");
    }

    public LoginPageViewModel()
	{
        TransferToSignUp = new Command(GoToSignUp);
	}
}