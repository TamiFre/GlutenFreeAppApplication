﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
        #endregion
    }
}
