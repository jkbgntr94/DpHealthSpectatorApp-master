using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms_EFCore.ViewModels.Drawing;

namespace Xamarin.Forms_EFCore.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddRoomsPage : ContentPage
	{
		public AddRoomsPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new AddRoomsViewModel();

        }
    }
}