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
    public class ChoosedDrugsPageViewModel : BaseViewModel
    {
        public ICommand AddNewDrug { get; private set; }
        public ICommand ToDashboard { get; private set; }

        private ObservableCollection<Lieky> _ContDrugList = new ObservableCollection<Lieky>();
        public ObservableCollection<Lieky> ContDrugList
        {
            get
            {
                return _ContDrugList;
            }
            set
            {
                _ContDrugList = value;
              
            }
        }


        private string nazov;
        public string Nazov
        {
            get { return nazov; }
            set
            {
                nazov = value;
                OnPropertyChanged();
            }
        }

        private DatabaseContext _context;

        public ChoosedDrugsPageViewModel(){
            _context = new DatabaseContext();
            AddNewDrug = new Command(addNewDrug);
            ToDashboard = new Command(toDashboard);
            FillDrugList();
        }

        public void FillDrugList()
        {
            ContDrugList.Clear();
            if (_context.Drugs.Any()) {
                var mojeLieky = _context.Drugs.ToList();
                int i = 0;
                System.Diagnostics.Debug.WriteLine("Moje som  tu ");
                foreach (var a in mojeLieky)
                {
                    System.Diagnostics.Debug.WriteLine("moje lieky" + i++ + a.Nazov);
                    ContDrugList.Add(a);


                }
            }else
            {
                Lieky liek = new Lieky
                {
                    Nazov = "V systéme nie je žiaden liek"

                };
                
                ContDrugList.Add(liek);

            }

            
        }

        async void addNewDrug()
        {
            FillDrugList();
            await Application.Current.MainPage.Navigation.PushAsync(new DrugsPickerPage());

        }

        async void toDashboard()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddRoomsPage());
        }
    }
}
