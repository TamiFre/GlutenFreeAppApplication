using GlutenFreeApp.ViewModel;
using System.ComponentModel;
using System.Windows.Input;
namespace GlutenFreeApp.Views;

public partial class SignUpView : ContentPage, INotifyPropertyChanged
{
    private bool isManagerVisible;
    private bool isManagerSelected;
    private bool isVisitorSelected;

    public bool IsManagerVisible
    {
        get => isManagerVisible;
        set
        {
            if (isManagerVisible != value)
            {
                isManagerVisible = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsManagerSelected
    {
        get => isManagerSelected;
        set
        {
            if (isManagerSelected != value)
            {
                isManagerSelected = value;
                IsManagerVisible = value;
                OnPropertyChanged(nameof(IsManagerSelected));
            }
        }
    }

    public bool IsVisitorSelected
    {
        get => isVisitorSelected;
        set
        {
            if (isVisitorSelected != value)
            {
                isVisitorSelected = value;
                OnPropertyChanged(nameof(IsVisitorSelected));
            }
        }
    }


    public ICommand ManagerSelectedCommand { get; }
    public ICommand VisitorSelectedCommand { get; }

    public SignUpView( SignUpViewModel vm)
	{ 
        this.BindingContext = vm;
		InitializeComponent();
        ManagerSelectedCommand = new Command(OnManagerSelected);
        VisitorSelectedCommand = new Command(OnVisitorSelected);
       

	}

    private void OnManagerSelected()
    {
        IsManagerSelected = true;
        IsVisitorSelected = false;
        IsManagerVisible = true;

    }

    private void OnVisitorSelected()
    {
        IsManagerSelected = false;
        IsVisitorSelected = true;
        IsManagerVisible = false;
    }

}