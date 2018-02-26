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

            initInstructions = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        }


        async void initStart()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new Phase1());

        }
    }
}
