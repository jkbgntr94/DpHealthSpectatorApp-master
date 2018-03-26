using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.ViewModels.Settings
{
    class EditProfileViewModel : BaseViewModel
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

        public EditProfileViewModel()
        {

            _context = new DatabaseContext();
            ToDrugs = new Command(toDrugs);

            fillValues();

        }

        private Profile prof;

        private void fillValues()
        {
            prof = _context.Profiles.FirstOrDefault(x => x.ProfileId == _context.Profiles.Max(p => p.ProfileId));

            if (prof == null) return;
            try
            {
                Username = prof.Meno;
                Surrname = prof.Priezvisko;
                Address = prof.Adresa;
                Weight = prof.Vaha;
                Height = prof.Vyska;
                BirthDate = DateTime.Parse(prof.DatumNarodenia);
                Poist = prof.Poistovna;
                Illnesses = prof.Ochorenia;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Plnenie obrazovky uprava profilu " + e.ToString());


            }
        }
            async void toDrugs()
            {

                SaveProfile();

                await Application.Current.MainPage.Navigation.PopAsync();

            }

            public void SaveProfile()
            {
                string sex = null;
                if (SexPicker == 0) { sex = "M"; }
                if (SexPicker == 1) { sex = "Z"; }


                    prof.Meno = Username;
                    prof.Priezvisko = Surrname;
                    prof.Adresa = Address;
                    prof.DatumNarodenia = BirthDate.ToString();
                    prof.Vaha = Weight;
                    prof.Vyska = Height;
                    prof.Pohlavie = sex;
                    prof.Ochorenia = Illnesses;
                    prof.Poistovna = Poist;
            
                if (_context.Profiles.Any())
                {
                    _context.Profiles.Update(prof);
                }
                else
                {

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

                }

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


