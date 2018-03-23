using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models.ObjectsForList;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels
{
    class EmailAlertSettingsViewModel : BaseViewModel
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;

            }
        }

        private string adress;
        public string Adress
        {
            get
            {
                return adress;
            }
            set
            {
                adress = value;

            }
        }

        private ObservableCollection<AlertsObj> listOfAlerts = new ObservableCollection<AlertsObj>();
        public ObservableCollection<AlertsObj> ListOfAlerts
        {
            get
            {
                return listOfAlerts;
            }
            set
            {
                listOfAlerts = value;

            }
        }

        private bool alertSwitchIsChecked;
        public bool AlertSwitchIsChecked
        {
            get { return alertSwitchIsChecked; }
            set
            {
                alertSwitchIsChecked = value;
                OnPropertyChanged();
            }
        }

        public ICommand toDash { get; private set; }


        public EmailAlertSettingsViewModel()
        {
            toDash = new Command(goNext);

            FillAlerts();

        }

        private void FillAlerts()
        {
            string[] listact = new string[6] { "Slabé", "Stredné", "Vysoké", "Dlho bez pohybu", "Prekročenie hranice", "Nastal pád" };
            int i = 0;
            foreach (var a in listact)
            {

                AlertsObj act = new AlertsObj()
                {   
                    id = i++,
                    Name = a,
                    AlertSwitchIsChecked = false
                };

                ListOfAlerts.Add(act);
            }

        }

        private void saveData()
        {

            SettingsController.Email = Adress;
            SettingsController.NameEmail = Name;

        }

        public void getListValues()
        {
            string[] listact = new string[6] { "Slabé", "Stredné", "Vysoké", "Dlho bez pohybu", "Prekročenie hranice", "Nastal pád" };

            foreach (var a in ListOfAlerts)
            {
                if(a.AlertSwitchIsChecked == true)
                {
                    switch (a.id)
                    {
                        case 0:
                            SettingsController.AlertLow = true;
                            break;
                        case 1:
                            SettingsController.AlertMiddle = true;
                            break;
                        case 2:
                            SettingsController.AlertHigh = true;
                            break;
                        case 3:
                            SettingsController.AlertMovementTime = true;
                            break;
                        case 4:
                            SettingsController.AlertMovement = true;
                            break;
                        case 5:
                            SettingsController.AlertFall = true;
                            break;

                    }

                    System.Diagnostics.Debug.WriteLine("CHECKED: " + a.Name);


                }

            }

        }

        async void goNext()
        {
            saveData();
            getListValues();
            await Application.Current.MainPage.Navigation.PushAsync(new DashboardPage());

        }

    }
}
