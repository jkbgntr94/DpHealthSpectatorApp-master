using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms_EFCore.Models.ObjectsForList;
using Xamarin.Forms_EFCore.ViewModels.Statistics;

namespace Xamarin.Forms_EFCore.Views.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StatisticVisualPage : ContentPage
	{
		public StatisticVisualPage ()
		{
			InitializeComponent ();
		}

        public StatisticVisualPage(RoomStatisticsObj roomStats)
        {
            InitializeComponent();
            BindingContext = new StatisticsVisualViewModel(roomStats);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new StatisticsVisualViewModel();
            
        }
    }
}