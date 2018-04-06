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
	public partial class StatisticsMainPage : ContentPage
	{
		public StatisticsMainPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new StatisticsMainViewModel();



        }
    }
}