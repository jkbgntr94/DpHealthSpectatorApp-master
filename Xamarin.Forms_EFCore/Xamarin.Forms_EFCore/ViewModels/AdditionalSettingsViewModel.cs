using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels
{
    public class AdditionalSettingsViewModel : BaseViewModel
    {
        public ICommand ToMainPage { get; private set; }
        private DatabaseContext _context;

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string time;
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged();
            }
        }

        public AdditionalSettingsViewModel()
        {
            _context = new DatabaseContext();
            ToMainPage = new Command(toMainPage);
            SettingsController.Email = Email;
            storeTimeToDb();


        }

        async void toMainPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HomeScreenPage());

        }

        private void storeTimeToDb()
        {
            if (!_context.MovementLimit.Any())
            {
                Hranice_Pohyb hp = new Hranice_Pohyb()
                {
                    HranicePohybId = 1,
                    LimitCas = Time,
                    TimeStamp = DateTime.Now.ToString("h:mm:ss tt")

                };

                _context.MovementLimit.Add(hp);

            }
            else
            {
                //najdi max a uloz zan
                Hranice_Pohyb limit = _context.MovementLimit.FirstOrDefault(t => t.HranicePohybId == _context.MovementLimit.Max(x => x.HranicePohybId));

                limit.LimitCas = Time;

                _context.MovementLimit.Update(limit);
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(AdditionalSettingsViewModel) + " " + e.ToString());
            }

        }
    }
}
