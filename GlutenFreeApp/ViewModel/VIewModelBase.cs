using System.ComponentModel;

namespace GlutenFreeApp.ViewModel;

public class ViewModelBase:INotifyPropertyChanged
{
	
	
      #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

}
