using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Views;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using Acr.UserDialogs;

namespace Xamarin.Forms_EFCore.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }
        private DatabaseContext _context;

        public LoginPageViewModel()
        {

            _context = new DatabaseContext();
            RegisterCommand = new Command(Register);
            LoginCommand = new Command(Login);

        }

        async void Register()
        {
            if (_context.Users.Any())
            {
                UserDialogs.Instance.Alert("V systéme existuje používateľ", "Nepovolená registrácia", "OK");

            }
            else
                await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());

        }

        async void Login()
        {
            
            try
            {
                var prihlas = _context.Users.Where(b => b.Login == Username).ToArray();
                System.Diagnostics.Debug.WriteLine(prihlas[0].Login + "Hello World!" + prihlas[0].Password);
                if ((prihlas[0].Login == Username) && (prihlas[0].Password == Password))
                {
                    UserDialogs.Instance.Alert("", "Úspešné prihlásenie", "OK");


                    if (_context.Profiles.Any())
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new DashboardPage());

                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new PacientNotFoundView());

                    }



                }
               


            } catch(Exception ee)
            {
                UserDialogs.Instance.Alert($"{Username} {Password}", "Neúspešné prihlásenie", "OK");
                //throw ee;
            }
            

            
           
        }
    }
}
