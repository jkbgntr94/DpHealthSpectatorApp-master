using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels
{
    class PacientNotFoundViewModel : BaseViewModel
    {
        public ICommand CreatePacientCommand { get; private set; }

        public PacientNotFoundViewModel()
        {

            CreatePacientCommand = new Command(CreatePacient);

        }

        async void CreatePacient()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new CreatePacientPage());
        }
    }
}
