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
      
        private string timeLimit;
        public string TimeLimit
        {
            get { return timeLimit; }
            set
            {
                timeLimit = value;
                OnPropertyChanged("TimeLimit");
            }
        }

        private string okruh;
        public string Okruh
        {
            get { return okruh; }
            set
            {
                okruh = value;
                OnPropertyChanged("Okruh");

            }
        }
        private TimeSpan sleepTime;
        public TimeSpan SleepTime
        {
            get { return sleepTime; }
            set
            {
                sleepTime = value;
                OnPropertyChanged();

            }
        }


        public ICommand emailYes { get; private set; }
        public ICommand emailNo { get; private set; }

        private DatabaseContext _context;


        public AdditionalSettingsViewModel()
        {
            _context = new DatabaseContext();
            emailNo = new Command(toMainPage);
            emailYes = new Command(toEmailSelection);

            


        }

        async void toMainPage()
        {
            storeValuesToDb();
            await Application.Current.MainPage.Navigation.PushAsync(new DashboardPage());

        }

        async void toEmailSelection()
        {
            storeValuesToDb();
            await Application.Current.MainPage.Navigation.PushAsync(new EmailAlertSettingsPage());

        }

        private void storeValuesToDb()
        {
            if (!_context.MovementLimit.Any())
            {
                Hranice_Pohyb hp = new Hranice_Pohyb()
                {
                    HranicePohybId = 1,
                    LimitCas = TimeLimit.ToString(),
                    OkruhHranica = Int32.Parse(Okruh),
                    Xhranica = SettingsController.MaxX,
                    Yhranica = SettingsController.MaxY,
                    TimeStamp = DateTime.Now.ToString("h:mm:ss tt")

                };

                _context.MovementLimit.Add(hp);

            }
            else
            {
                //najdi max a uloz zan
                Hranice_Pohyb limit = _context.MovementLimit.FirstOrDefault(t => t.HranicePohybId == _context.MovementLimit.Max(x => x.HranicePohybId));

                int index = limit.HranicePohybId;
                index++;

                Hranice_Pohyb hp = new Hranice_Pohyb()
                {
                    HranicePohybId = index,
                    LimitCas = TimeLimit.ToString(),
                    OkruhHranica = Int32.Parse(Okruh),
                    Xhranica = SettingsController.MaxX,
                    Yhranica = SettingsController.MaxY,
                    TimeStamp = DateTime.Now.ToString("h:mm:ss tt")

                };

                _context.MovementLimit.Add(hp);

            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(AdditionalSettingsViewModel) + " " + e.ToString());
            }

            SettingsController.SleepTime = SleepTime.ToString();

        }
    }
}
