
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
    public class CreatePacientViewModel : BaseViewModel
    {

        public ICommand ToDrugs { get; private set; }
        private DatabaseContext _context;


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

        private string surrname;
        public string Surrname
        {
            get { return surrname; }
            set
            {
                surrname = value;
                OnPropertyChanged();
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }

        private float weight;
        public float Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged();
            }
        }

        private float height;
        public float Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged();
            }
        }

        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged();
            }
        }

        /*
         0 - muz
         1 - zena
         */
        private int sexPicker;
        public int SexPicker
        {
            get { return sexPicker; }
            set
            {
                sexPicker = value;
                OnPropertyChanged();
            }
        }

        private string poist;
        public string Poist
        {
            get { return poist; }
            set
            {
                poist = value;
                OnPropertyChanged();
            }
        }

        private string illnesses;
        public string Illnesses
        {
            get { return illnesses; }
            set
            {
                illnesses = value;
                OnPropertyChanged();
            }
        }

        public CreatePacientViewModel()
        {

            _context = new DatabaseContext();
            ToDrugs = new Command(toDrugs);
           

        }

        async void toDrugs()
        {

            SaveProfile();

            await Application.Current.MainPage.Navigation.PushAsync(new ChoosedDrugsPage());

        }

        private void SaveProfile()
        {
            string sex = null;
            if (SexPicker == 0) { sex = "M"; }
            if (SexPicker == 1) { sex = "Z"; }

            Profile profile = new Profile()
            {
                ProfileId = 1,
                Meno = Username,
                Priezvisko = Surrname,
                Adresa = Address,
                DatumNarodenia = BirthDate.ToString(),
                Vaha = Weight,
                Vyska = Height,
                Pohlavie = sex,
                Ochorenia = Illnesses,
                Poistovna = Poist

            };
            
            
            _context.Profiles.Add(profile);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
