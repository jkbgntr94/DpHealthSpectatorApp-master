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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            userimage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.user_plus.png");
            loginimage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.login2.png");

            BindingContext = new LoginPageViewModel();
        }

	}
}