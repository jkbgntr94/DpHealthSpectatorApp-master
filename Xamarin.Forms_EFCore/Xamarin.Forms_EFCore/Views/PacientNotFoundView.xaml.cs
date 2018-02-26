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
	public partial class PacientNotFoundView : ContentPage
	{
		public PacientNotFoundView ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            riskimage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.Risk.png");
            idcardimage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.id_card.png");

            BindingContext = new PacientNotFoundViewModel();
        }
    }
}