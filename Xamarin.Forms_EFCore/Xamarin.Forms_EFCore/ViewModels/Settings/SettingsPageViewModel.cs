using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers.SekvenceHelper;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Views.Settings;

namespace Xamarin.Forms_EFCore.ViewModels.Settings
{
    public class SettingsPageViewModel
    {
        public ICommand EditProfileCommand { get; private set; }
        public ICommand RoomStatsCommand{ get; private set; }

        public ICommand EditAlertsCommand { get; private set; }
        public ICommand EditEmailAlertsCommand { get; private set; }
        public ICommand editBordersCommand { get; private set; }



        public SettingsPageViewModel()
        {
            EditProfileCommand = new Command(editProfileCommand);
            EditAlertsCommand = new Command(editAlertsCommand);
            RoomStatsCommand = new Command(roomStatsCommand);
            EditEmailAlertsCommand = new Command(editEmailAlertsCommand);
            editBordersCommand = new Command(EmailAlertsCommand);

        }

        async void EmailAlertsCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditBordersPage());

        }

        async void editProfileCommand()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new EditProfilePage());


        }

        async void editEmailAlertsCommand()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new EditEmailAlertPAge());


        }
        private void roomStatsCommand()
        {

            DatabaseContext context = new DatabaseContext();

            var SekvList = context.MovementSekv.ToList();

            var results = SekvList.GroupBy(
                p => p.IzbyFK);

            String alertText = "";

            foreach (var r in results)
            {

                try { 
                    Izby izba = context.Rooms.Where(p => p.IzbaID == r.Key).First();
                    System.Diagnostics.Debug.WriteLine("ROOMS: " + izba.Nazov + " -- " + r.Count());
                    alertText += izba.Nazov + ": " + r.Count() + " sekvencie\n";
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("ROOMS STATISTIC: " + e.ToString());
                    System.Diagnostics.Debug.WriteLine("ROOMS: " + " NA " + " -- " + r.Count());
                    alertText += "NA" + ": " + r.Count() + " sekvencie\n";
                }

                

            }

            UserDialogs.Instance.Alert(alertText, "Štatistika pohybu", "OK");

            //await Application.Current.MainPage.Navigation.PushAsync(new EditProfilePage());


        }

        async void editAlertsCommand()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new EditAlertsPage());

        }
    }
}
