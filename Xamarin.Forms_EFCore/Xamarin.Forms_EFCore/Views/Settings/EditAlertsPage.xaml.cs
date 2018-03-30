using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms_EFCore.ViewModels.Settings;

namespace Xamarin.Forms_EFCore.Views.Settings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditAlertsPage : ContentPage
	{
		public EditAlertsPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            BindingContext = new EditAlertsViewModel();
        }
    }
}