using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using GlutenFreeApp.Models;
using GlutenFreeApp.Services;

namespace GlutenFreeApp.ViewModel
{
    public class InformationViewModel:ViewModelBase
    {
        //to do:
        //show the facts in a label that will rotate every few seconds in an animated way
        //take the facts from the information table in the DB in a random order


        private GlutenFreeServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        #region builder
        public InformationViewModel(GlutenFreeServiceWebAPIProxy proxy, IServiceProvider serviceProvider) 
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            FillAllFunFacts();
            timer = new System.Timers.Timer(10000); // Set timer interval to 30 seconds - change later
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
            GetRandomFactCommand = new Command(GetRandomFact);
        }
        #endregion

        #region properties
        private string currentFact;
        public string CurrentFact
        {
            get { return currentFact; }
            set { currentFact = value; OnPropertyChanged(); }
        }

        private ObservableCollection<InformationInfo> infoCol;
        public ObservableCollection<InformationInfo> InfoCol
        {
            get { return infoCol; }
            set { infoCol = value; OnPropertyChanged(); }
        }
        #endregion

        #region timer and fill observable collection

        private System.Timers.Timer timer;
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Change fact every 30 seconds
            SetRandomFact();
        }

        public void GetRandomFact()
        {
            // This will get a random fact when the button is clicked
            SetRandomFact();
        }

        private void SetRandomFact()
        {
            Random random = new Random();
            var fact = InfoCol[random.Next(InfoCol.Count)];
            CurrentFact = fact.InfoText;
        }

        public async void FillAllFunFacts()
        {
            List<InformationInfo> list = await proxy.GetAllFunFacts();
            InfoCol = new ObservableCollection<InformationInfo>(list);
        }

        public ICommand GetRandomFactCommand { get; set; }

        #endregion
    }
}
