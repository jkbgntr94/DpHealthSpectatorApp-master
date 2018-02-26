using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels
{
    public class InitialPageViewModel : BaseViewModel
    {
        private decimal progressValue;
        public decimal ProgressValue
        {
            get { return progressValue; }
            set
            {
                progressValue = value;
                OnPropertyChanged();
            }
        }


        public InitialPageViewModel()
        {
            new Thread(new ThreadStart(delegate
            {
               while(ProgressValue < 1)
                {
                    ProgressValue += 0.1M;
                    Thread.Sleep(100);

                }
               
            })).Start();

            Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

        }


    }
}
