using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class AddTimePageViewModel : BaseViewModel
    {

        public ICommand AddNewTime { get; private set; }
        public ICommand SetTimes { get; private set; }

        private ObservableCollection<Cas_Davkovania> _ContTimesList = new ObservableCollection<Cas_Davkovania>();
        public ObservableCollection<Cas_Davkovania> ContTimesList
        {
            get
            {
                return _ContTimesList;
            }
            set
            {
                _ContTimesList = value;

            }
        }

        private TimeSpan doseTime;
        public TimeSpan DoseTime
        {
            get { return doseTime; }
            set
            {
                doseTime = value;
                OnPropertyChanged();
            }
        }

        DatabaseContext _context;
        public AddTimePageViewModel()
        {
            _context = new DatabaseContext();
            
            AddNewTime = new Command(addNewTime);
            SetTimes = new Command(setTimes);

        }

        async void addNewTime()
        {
            
            int index = GetDrugIndex();

            try
            {
                System.Diagnostics.Debug.WriteLine(DoseTime.ToString());
                Cas_Davkovania davk = new Cas_Davkovania()
                {
                    Cas = DoseTime.ToString(),
                    LiekyFK = index

                };

                _context.DrugTakeTime.Add(davk);

            }
            catch (Exception e)
            {
                throw e;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception f)
            {
                throw f;
            }
            LoadTimes();

        }

        private int GetDrugIndex()
        {

            int index;

            if (!_context.Drugs.Any())
            {
                index = 1;
            }
            else
            {
                Lieky tmp = _context.Drugs.FirstOrDefault(t => t.LiekId == _context.Drugs.Max(x => x.LiekId));

                index = tmp.LiekId;
            }

            return index;


        }


        private void LoadTimes()
        {
            ContTimesList.Clear();
            int index = GetDrugIndex();
            var casy = _context.DrugTakeTime.Where(t => t.LiekyFK == index).ToList();
            foreach (var c in casy)
            {
                ContTimesList.Add(c);
            }


        }

        async void setTimes()
        {
            UserDialogs.Instance.Alert("Čas užívania bol pridaný", "", "OK");

            await Application.Current.MainPage.Navigation.PushAsync(new ChoosedDrugsPage());

        }

    }
}
