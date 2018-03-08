using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms_EFCore.ViewModels
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovementVisualPage : ContentPage
	{
		public MovementVisualPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            movImg.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.movement.png");

            tempImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.temperature.png");
            pulseImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.pulse.png");
            dashboardImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.home.png");
            movementImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.movement.png");
            fallImage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.fall.png");

            BindingContext = new MovementVisualViewModel();
        }
    }
}