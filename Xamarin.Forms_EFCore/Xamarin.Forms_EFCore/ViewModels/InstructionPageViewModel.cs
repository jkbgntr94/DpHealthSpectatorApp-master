using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels
{
    public class InstructionPageViewModel : BaseViewModel
    {
        private string initInstructions;
        public string InitInstructions
        {
            get { return initInstructions; }
            set
            {
                initInstructions = value;
                OnPropertyChanged();
            }
        }

        DatabaseContext _context;

        public ICommand InitStart { get; private set; }

        public InstructionPageViewModel()
        {

            _context = new DatabaseContext();
            InitStart = new Command(initStart);

            initInstructions = "Teraz nasleduje inicializácia hraníc pre TEP. Proces je rozdelený do dvoch fáz. Prvá fáza trvá 15 minút. Počas tejto fázy je potrebné, aby pacient ostal v kľude a nepodliehal stresu. Senzory budú postupne zaznamenávať hodnoty tepu, z ktorých budú neskôr určené hranice pre upozornenia.\n\n" +
                                "Druhá fáza aplikácie obsahuje konkrétne činnosti. Zo zoznamu vyberte aké činnosti chcete zaznamenať a pri ktorých z nich môže pacient pocítiť stres. Každá z činností sa zaznamenáva 10 minút.\n\n" +
                                "Pre pokračovanie stlačte tlačidlo na spodu obrazovky.Po jeho stlačení sa automaticky spustí prvá fáza.";
        }


        async void initStart()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new Phase1());

        }
    }
}
