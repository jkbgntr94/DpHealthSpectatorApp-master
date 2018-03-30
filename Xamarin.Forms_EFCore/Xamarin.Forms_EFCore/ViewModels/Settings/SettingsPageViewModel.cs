using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.Views.Settings;

namespace Xamarin.Forms_EFCore.ViewModels.Settings
{
    public class SettingsPageViewModel
    {
        public ICommand EditProfileCommand { get; private set; }

        public ICommand EditAlertsCommand { get; private set; }

        public SettingsPageViewModel()
        {
            EditProfileCommand = new Command(editProfileCommand);
            EditAlertsCommand = new Command(editAlertsCommand);

        }

        async void editProfileCommand()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new EditProfilePage());


        }

        async void editAlertsCommand()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new EditAlertsPage());

        }
    }
}
