using System.Windows.Input;
using GlutenFreeApp.Models;
using GlutenFreeApp.Services;
namespace GlutenFreeApp.ViewModel;

public class LoginPageViewModel : ViewModelBase
{


    private GlutenFreeServiceWebAPIProxy proxy;
    private IServiceProvider serviceProvider;

    #region loginpage builder
    public LoginPageViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.proxy = proxy;
        TransferToSignUp = new Command(GoToSignUp);
        LoginCommand = new Command(OnLogin);
        password = "";
        InServerCall = false;
        errorMsg = "";
    }
    #endregion

    #region login command
    //login button
    public ICommand LoginCommand { get; set; }

    //OnLogin
    private async void OnLogin()
    {
        //Choose the way you want to blobk the page while indicating a server call
        InServerCall = true;
        ErrorMsg = "";
        //Call the server to login
        UsersInfo loginInfo = new UsersInfo { Name = UserName, Password = Password };
        UsersInfo? u = await this.proxy.LoginAsync(loginInfo);

        InServerCall = false;

        //Set the application logged in user to be whatever user returned (null or real user)
        ((App)Application.Current).LoggedInUser= u;
        if (u == null)
        {
            ErrorMsg = "Invalid name or password";
        }
        else
        {
            ErrorMsg = "";
            //Navigate to the main page
            AppShell shell = serviceProvider.GetService<AppShell>();

            //TasksViewModel tasksViewModel = serviceProvider.GetService<TasksViewModel>();
            //tasksViewModel.Refresh(); //Refresh data and user in the tasksview model as it is a singleton

            ((App)Application.Current).MainPage = shell;
            Shell.Current.FlyoutIsPresented = false; //close the flyout

            //currently navigates to the recipes and later add the info

            Shell.Current.GoToAsync("AllRecipes"); //Navigate to the Info tab page - add later
        }
    }
    #endregion


    #region properties
    //error massage

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



    //username
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



    //password
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
    #endregion

    #region sign up redirect
    //redirect to sign up
    public ICommand TransferToSignUp { get; set; }


    //ASK OFER - IT WONT WORK BECAUSE NO SHELL SO HOW DO I DO THAT

    private async void GoToSignUp()
    {
        AppShell.Current.GoToAsync("///SignUp");
    }
    #endregion


}