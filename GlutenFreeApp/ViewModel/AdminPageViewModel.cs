using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GlutenFreeApp.Models;
using GlutenFreeApp.Services;



namespace GlutenFreeApp.ViewModel
{
    public class AdminPageViewModel:ViewModelBase
    {
        //to do:
        //do a command that adds the fact the admin wants to add to the DB
        //do a sort of table that shows all of the pending restaurants and allow the admin to approve them
        //order: first the facts and then do the table - example might be in talsi's git with the monkeys

        //ADD BINDING

        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        #region builder
        public AdminPageViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider) 
        {
            AddFactCommand = new Command(AddFact);
        }
        #endregion

        #region properties
        private string fact;
        public string Fact 
        {
            get => fact;
            set 
            {
                if (fact!=value)
                {
                    fact = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion



        #region buttons
        public ICommand AddFactCommand { get; set; }
        #endregion

        private async void AddFact()
        {
            InServerCall = true;

            //call the server to add the information

            InformationInfo information = new InformationInfo { InfoText = Fact};
            bool worked = await this.proxy.AddFactAsync(information);
            InServerCall = false;
            
           
        }

    }
}
