using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Helpers;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using Acr.UserDialogs;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels
{
    public class DrugsPickerPageViewModel : BaseViewModel
    {

       


        private string drugName;
        public string DrugName
        {
            get { return drugName; }
            set
            {
                drugName = value;
                OnPropertyChanged();
            }
        }

        private int drugDose;
        public int DrugDose
        {
            get { return drugDose; }
            set
            {
                drugDose = value;
                OnPropertyChanged();
            }
        }

        private int repeatedDrug;
        public int RepeatedDrug
        {
            get { return repeatedDrug; }
            set
            {
                repeatedDrug = value;
                OnPropertyChanged();
            }
        }

        private string note;
        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                OnPropertyChanged();
            }
        }

        
        DatabaseContext _context;

        public ICommand SaveNewDrug { get; private set; }

        public DrugsPickerPageViewModel()
        {
            _context = new DatabaseContext();
            SaveNewDrug = new Command(saveNewDrug);
            

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

                index = tmp.LiekId + 1;
            }

            return index;


        }


        async void saveNewDrug()
        {

            /*
             TODO: Dorobit navazovanie na lognuteho usera
             */
            int index = GetDrugIndex();

            try
            {
               
                Lieky newDrug = new Lieky()
                {
                    LiekId = index,
                    Nazov = DrugName,
                    Pocet = DrugDose,
                    Dlhodobo = RepeatedDrug,
                    Poznamka = Note
                };
                _context.Drugs.Add(newDrug);
            }
            catch(Exception s)
            {
                throw s;
            }
            

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

            UserDialogs.Instance.Alert("Liek bol pridaný", "", "OK");
            await Application.Current.MainPage.Navigation.PushAsync(new AddTimesPage());

        }
        


    }
}
