using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms_EFCore.ViewModels;

namespace Xamarin.Forms_EFCore.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FinalInitializationPage : ContentPage
	{
		public FinalInitializationPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            myLocalImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.done.png");

            ResetNavigationStack();
            BindingContext = new Final_InitializationViewModel();
        }
        public void ResetNavigationStack()
        {
            if (Navigation != null && Navigation.NavigationStack.Count() > 0)
            {
                var existingPages = Navigation.NavigationStack.ToList();

                for(int i = 0; i < existingPages.Count - 1; i++)
                {
                    Navigation.RemovePage(existingPages[i]);
                }
                
            }
        }
    }
}