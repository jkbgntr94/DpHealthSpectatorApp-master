using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels
{
    public class Phase2StressViewModel : BaseViewModel
    {
        private Queue<Activities> _ListOfSelectedActivity { get; set; }
        public Queue<Activities> ListOfSelectedActivity
        {
            get
            {
                return _ListOfSelectedActivity;
            }
            set
            {
                _ListOfSelectedActivity = value;

            }
        }

        private string stressDescText;
        public string StressDescText
        {
            get { return stressDescText; }
            set
            {
                stressDescText = value;
                OnPropertyChanged();
            }
        }


        public ICommand ToNextPhase { get; private set; }


        public Phase2StressViewModel(Queue<Activities> aktivity_in)
        {
            ToNextPhase = new Command(toPhase2);
            StressDescText = "Vyberte, pri ktorých aktivitách mohol pacient pocítiť stres";
            ListOfSelectedActivity = aktivity_in;

        }

        async void toPhase2()
        {


            await Application.Current.MainPage.Navigation.PushAsync(new Phase2Page(ListOfSelectedActivity));

        }


    }
}
