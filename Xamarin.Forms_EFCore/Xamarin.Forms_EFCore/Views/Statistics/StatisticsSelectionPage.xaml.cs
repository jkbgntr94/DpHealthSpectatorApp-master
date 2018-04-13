using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms_EFCore.ViewModels.Statistics;

namespace Xamarin.Forms_EFCore.Views.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StatisticsSelectionPage : ContentPage
	{
		public StatisticsSelectionPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();

            arrowimage.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.arrow.png");
            arrowimage1.Source = ImageSource.FromResource("Xamarin.Forms_EFCore.arrow.png");
            

            BindingContext = new StatisticsSelectionViewModel();



        }
    }
}