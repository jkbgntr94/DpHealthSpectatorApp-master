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
       

        async void editAlertsCommand()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new EditAlertsPage());

        }
    }
}
