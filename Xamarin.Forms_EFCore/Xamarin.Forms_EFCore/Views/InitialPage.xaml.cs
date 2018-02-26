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
	public partial class InitialPage : ContentPage
	{
		public InitialPage ()
		{
			InitializeComponent ();
           

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            myLocalImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.logo.jpg");

            //MainProgressBar.ProgressTo(1.0, 1800, Easing.Linear);

            BindingContext = new InitialPageViewModel();
        }
    }
}