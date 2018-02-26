using System;
using System.Collections.Generic;
using System.Text;
using Plugin.SecureStorage;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;
using Acr.UserDialogs;
using System.Linq;

namespace Xamarin.Forms_EFCore.ViewModels
{
    class RegisterPageViewModel : BaseViewModel
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

        private string repeatedPassword;
        public string RepeatedPassword
        {
            get { return repeatedPassword; }
            set
            {
                repeatedPassword = value;
                OnPropertyChanged();
            }
        }

        

        public ICommand RegisterCommand { get; private set; }
        private DatabaseContext _context;

        public RegisterPageViewModel()
        {

            _context = new DatabaseContext();
            RegisterCommand = new Command(SaveUser);

        }


        async void SaveUser()
        {

            if (Password == RepeatedPassword) {

                AesCrypto cryptModul = new AesCrypto();
                User[] prihlas; 
                try
                {
                     prihlas = _context.Users.Where(b => b.Login == Username).ToArray();
                }
                catch (Exception e)
                {
                    throw e;
                }

                bool empty = false;
                try
                {
                    if (prihlas[0].Login == Username)
                    {
                        empty = true;
                    }
                }
                catch (Exception e)
                {
                    //throw e;
                }


                //System.Diagnostics.Debug.WriteLine("prihlas " + prihlas[1].Login);

                if (empty)
                    {
                        UserDialogs.Instance.Alert("Login", "Obsadené meno", "OK");
                        prihlas = null;
                    }
                    else
                    {


                       
                        byte[] kluc = cryptModul.GetKey();
                        string klucik = Convert.ToBase64String(kluc);


                        byte[] klucIV = cryptModul.GetIV();
                        string klucikIV = Convert.ToBase64String(klucIV);

                       
                        User user = new User
                        {

                            Login = Username,
                            Password = Password,
                            Key = klucik,
                            IV = klucikIV,


                        };
                        _context.Users.Add(user);


                        try
                        {
                            _context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        UserDialogs.Instance.Alert("Úspešná registrácia", "Registrácia", "OK");
                        await Application.Current.MainPage.Navigation.PopAsync();
                        
                    }


                
                
            } else {
                UserDialogs.Instance.Alert("Heslo sa nezhoduje", "Chyba", "OK");

            }

        }

     
    }

}
