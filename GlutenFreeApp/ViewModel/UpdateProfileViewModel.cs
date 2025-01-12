using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GlutenFreeApp.Services;
using GlutenFreeApp.Models;

namespace GlutenFreeApp.ViewModel
{
    public class UpdateProfileViewModel:ViewModelBase
    {
        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public UpdateProfileViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider) 
        {
            UsersInfo currentUser = ((App)App.Current).LoggedInUser;
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            SubmitCommand = new Command(OnSubmit);
            PasswordError = "Password must be at least 4 characters long and contain letters and numbers";

        }

        #region properties
        private string userName;
        public string UserName 
        {
            get => userName;
            set 
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged();
                }
            }
        }

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

        #endregion

        #region submit
        public ICommand SubmitCommand { set; get; }
        private async void OnSubmit()
        {
            ValidatePassword();
            UsersInfo currentUser = ((App)App.Current).LoggedInUser;
            if (!ShowPasswordError)
            {
                currentUser.Password = Password;
                currentUser.Name = UserName;
            }
            InServerCall = true;
            bool success = await proxy.UpdateUser(currentUser);
            if (success)
            {
                await Application.Current.MainPage.DisplayAlert("Profile Updated", "Success", "ok");
                
                //HOW TO REMOVE THE SHELL
                Shell.Current.GoToAsync("Login");
                
            }
        }
        #endregion
    }
}
