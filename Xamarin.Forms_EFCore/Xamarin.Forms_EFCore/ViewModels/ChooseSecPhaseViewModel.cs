using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels
{
    public class ChooseSecPhaseViewModel : BaseViewModel
    {
        public int zvolene;

        private Queue<Activities> _ListOfActivity = new Queue<Activities>();
        public Queue<Activities> ListOfActivity
        {
            get
            {
                return _ListOfActivity;
            }
            set
            {
                _ListOfActivity = value;

            }
        }

        private string descText;
        public string DescText
        {
            get { return descText; }
            set
            {
                descText = value;
                OnPropertyChanged();
            }
        }

        private bool actSwitchIsChecked;
        public bool ActSwitchIsChecked
        {
            get { return actSwitchIsChecked; }
            set
            {
                actSwitchIsChecked = value;
                OnPropertyChanged();
            }
        }

        public ICommand ToNextPhases { get; private set; }

        public ChooseSecPhaseViewModel()
        {
           
            ToNextPhases = new Command(toNextPhases);
            DescText = "Vyberte často vykonávané činnosti pacienta, ktoré chcete použiť pri druhej fáze inicializície";
            FillActivities();
           
           // System.Diagnostics.Debug.WriteLine("sasasa " + ActSwitchIsChecked);


        }


        async void toNextPhases()
        {
            Queue<Activities> aktivity = ListOfActivity;
            Queue<Activities> aktivityCorr = new Queue<Activities>();
            zvolene = 0;
            foreach (var aktiv in aktivity)
            {
               // System.Diagnostics.Debug.WriteLine("aktivitka " + aktiv.ActSwitchIsChecked + " " + aktiv.Name);
                if (aktiv.ActSwitchIsChecked == true)
                {
                    zvolene++;
                    aktivityCorr.Enqueue(aktiv);

                }
            }
            await Application.Current.MainPage.Navigation.PushAsync(new Phase2StressPage(aktivityCorr));

        }

        public void FillActivities()
        {
            string[] listact = new string[5] { "Sledovanie TV", "Čítanie novín", "Ležanie na posteli", "Varenie", "Oddych na balkóne" };
            int i = 1;
            foreach (var a in listact)
            {

                Activities act = new Activities()
                {
                    
                    Name = a
                    
                    
                };

                ListOfActivity.Enqueue(act);
            }

    }
    }
}
