using GlutenFreeApp.ViewModel;
using System.ComponentModel;
using System.Windows.Input;
namespace GlutenFreeApp.Views;

public partial class SignUpView : ContentPage
{
    public SignUpView(SignUpViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

}