using MailKit.Net.Smtp;
using MimeKit;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.Helpers;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels
{
    public class AlertSelectionViewModel : BaseViewModel
    {
        public ICommand SaveAlerts { get; private set; }

        /*
        0- Push notifikácia
        1- Notifikácia správou -- uz nie je, posunie sa to na 0,1
        2- Zvukový alarm
        */
        private int alertPulseSlabe;
        public int AlertPulseSlabe
        {
            get { return alertPulseSlabe; }
            set
            {
                alertPulseSlabe = value;
                OnPropertyChanged();
            }
        }

        private int alertPulseStredne;
        public int AlertPulseStredne
        {
            get { return alertPulseStredne; }
            set
            {
                alertPulseStredne = value;
                OnPropertyChanged();
            }
        }

        private int alertPulseVysoke;
        public int AlertPulseVysoke
        {
            get { return alertPulseVysoke; }
            set
            {
                alertPulseVysoke = value;
                OnPropertyChanged();
            }
        }

        private int longTimeNoMovement;
        public int LongTimeNoMovement
        {
            get { return longTimeNoMovement; }
            set
            {
                longTimeNoMovement = value;
                OnPropertyChanged();
            }
        }

        private int signalLost;
        public int SignalLost
        {
            get { return signalLost; }
            set
            {
                signalLost = value;
                OnPropertyChanged();
            }
        }

        private int fallDetected;
        public int FallDetected
        {
            get { return fallDetected; }
            set
            {
                fallDetected = value;
                OnPropertyChanged();
            }
        }



        public AlertSelectionViewModel()
        {

            SaveAlerts = new Command(saveAlerts);

        }

        public void saveAlerts()
        {
            
            SettingsController.AlertPulseSlabe = AlertPulseSlabe;
            SettingsController.AlertPulseStredne = AlertPulseStredne;
            SettingsController.AlertPulseVysoke = AlertPulseVysoke;
            SettingsController.LongTimeNoMovement = LongTimeNoMovement;
            SettingsController.SignalLost = SignalLost;
            SettingsController.FallDetected = FallDetected;

            //Application.Current.MainPage.Navigation.PopModalAsync();
            Application.Current.MainPage.Navigation.PushAsync(new AdditionalSettingsPage());

            //Console.WriteLine("seruuuusssssssssssss {0} {1} {2} {3} {4} {5}", Settings.AlertPulseSlabe, Settings.AlertPulseStredne, Settings.AlertPulseVysoke, Settings.LongTimeNoMovement, Settings.SignalLost, Settings.FallDetected);

            //TODO: screen kde prida ostatne potrebne nastavenia, cas pre pohyb, email 

            //SendEmailMessage();
            //GeneratePushAlert();
        }

    }
}
